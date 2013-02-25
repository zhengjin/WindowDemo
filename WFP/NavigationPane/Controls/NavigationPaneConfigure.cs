#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using NavigationPane.Properties;
using Stema.Controls.Converters;
using Stema.Utils;
using System;

namespace Stema.Controls
{
 public enum ConfiguretMenuItemType
 {
  FewerButtons,
  MoreButtons,
  Options,
  HiddenItems,
  AddRemove,
  NewWindow,
  MenuItem
 }

 public enum ConfigureMenuSeparator
 {
  Top,
  Bottom,
  Both,
  None
 }

 public class ConfigureMenuItem : MenuItem
 {
  /// <summary>
  /// Specify if Style shuld be corrected in case it as been overriden
  /// this allow the Style to reflect changes done in overrides
  /// </summary>
  public bool CorrectDefaultStyle
  {
   get { return (bool)GetValue(CorrectDefaultStyleProperty); }
   set { SetValue(CorrectDefaultStyleProperty, value); }
  }
  public static readonly DependencyProperty CorrectDefaultStyleProperty =
      DependencyProperty.Register("CorrectDefaultStyle", typeof(bool), typeof(ConfigureMenuItem), new UIPropertyMetadata(true));

  public ConfiguretMenuItemType MenuType
  {
   get { return (ConfiguretMenuItemType)GetValue(MenuTypeProperty); }
   set { SetValue(MenuTypeProperty, value); }
  }
  public static readonly DependencyProperty MenuTypeProperty =
      DependencyProperty.Register("MenuType", typeof(ConfiguretMenuItemType), typeof(ConfigureMenuItem), new UIPropertyMetadata(ConfiguretMenuItemType.MenuItem));

  public ConfigureMenuSeparator Separator
  {
   get { return (ConfigureMenuSeparator)GetValue(SeparatorProperty); }
   set { SetValue(SeparatorProperty, value); }
  }
  public static readonly DependencyProperty SeparatorProperty =
      DependencyProperty.Register("Separator", typeof(ConfigureMenuSeparator), typeof(ConfigureMenuItem), new UIPropertyMetadata(ConfigureMenuSeparator.None));

  private Image GetImageFromResourceKey(string resourceId)
  {
   Image image = null;
   ImageSource imageSource = TryFindResource(new ComponentResourceKey(typeof(NavigationPane), resourceId)) as ImageSource;
   if (imageSource != null)
   {
    image = new Image();
    image.Source = imageSource;
   }
   return image;
  }

  private string GetFluentTrickedString(string str)
  {
   if(fluentTrickTrick)
   {
    return str.Replace("_", "");
   }
   return str;
  }

  private bool fluentTrickTrick;
  private void CheckFluent()
  {
   // here we go tricky - tricky
   // Fluent library as a problem with acces text in the MenuItem Style the made...
   // "_A" is shown as "_A" instead of an underscored A !!
   // so...
   // if you are using the navigationPane with Fluent for the ribbon
   // you probably don't want tah "_" in the menu voices !!
   //
   // Here is the Trick:
   // we look for a Fluent control type ( Fluent.ContextMenu )
   // if fluent assembly is loaded the type gets found
   Type t = Type.GetType("Fluent.ContextMenu, Fluent");
   // if ThemeDictionaryExtension type exists we try to find its style !
   // since you can also have the menuitem in a window without 
   // fluent style resources merged
   if (t != null)
    fluentTrickTrick = TryFindResource(t) != null;
   else
    fluentTrickTrick = false;
  }

  private void SetValues()
  {
   CheckFluent();
   Image image = null;
   switch (MenuType)
   {
    case ConfiguretMenuItemType.FewerButtons:
     Header = GetFluentTrickedString(NPR.NavigationPane_FewerCommand);
     image = GetImageFromResourceKey("NavigationPane_FewerIcon");
     if (image == null)
     {
      // no fewer imagesource specified... just try with more icon and if found flips it vertically
      image = GetImageFromResourceKey("NavigationPane_MoreIcon");
      if (image != null)
      {
       TransformGroup tg = new TransformGroup();
       tg.Children.Add(new ScaleTransform(1, -1));
       image.SnapsToDevicePixels = true;
       image.LayoutTransform = tg;
      }
     }
     Icon = image;
     Command = NavigationPane.ConfigureCommand;
     CommandParameter = "fewer";
     CommandTarget = _navigationPane;
     break;
    case ConfiguretMenuItemType.MoreButtons:
     Header = GetFluentTrickedString(NPR.NavigationPane_MoreCommand);
     Icon = GetImageFromResourceKey("NavigationPane_MoreIcon");
     Command = NavigationPane.ConfigureCommand;
     CommandParameter = "more";
     CommandTarget = _navigationPane;
     break;
    case ConfiguretMenuItemType.Options:
     Header = GetFluentTrickedString(NPR.NavigationPane_OptionsCommand);
     Command = NavigationPane.ConfigureCommand;
     CommandParameter = "options";
     CommandTarget = _navigationPane;
     break;
    case ConfiguretMenuItemType.NewWindow:
     Header = GetFluentTrickedString(NPR.NavigationPane_NewWindowCommand);
     Icon = GetImageFromResourceKey("NavigationPane_NewWindowIcon");
     Command = NavigationPane.ConfigureCommand;
     CommandParameter = "NewWindow";
     CommandTarget = _navigationPane;
     break;
    case ConfiguretMenuItemType.AddRemove:
     Header = GetFluentTrickedString(NPR.NavigationPane_AddRemoveCommand);
     if (_navigationPane != null)
     {
      _contextMenu.Opened +=new RoutedEventHandler(_contextMenu_Opened);
     }
     break;
    case ConfiguretMenuItemType.HiddenItems:
     _contextMenu.Opened += new RoutedEventHandler(_contextMenu_Opened);
     Visibility = System.Windows.Visibility.Collapsed;
     Header = "Hidden Items Place Holder";
     break;
   }
  }

  private MenuItem MenuItemFromNavigationPaneItem(NavigationPaneItem item, bool isHiddenItem)
  {
   MenuItem menuItem = new MenuItem();

   menuItem.IsCheckable = true;

   Binding b = new Binding();
   b.Source = item;
   if (isHiddenItem)
    b.Path = new PropertyPath(Selector.IsSelectedProperty);
   else
   {
    b.Path = new PropertyPath(NavigationPane.IsItemExcludedProperty);
    b.Converter = new BooleanNegateConverter();
   }
   b.Mode = BindingMode.TwoWay;

   menuItem.SetBinding(MenuItem.IsCheckedProperty, b);

   if (item.ImageSmall != null)
   {
    Image image = new Image();
    image.Width = 16;
    image.Height = 16;
    image.Source = item.ImageSmall;
    menuItem.Icon = image;
   }
   menuItem.Header = XamlHelper.CloneUsingXaml(item.Header, true);

   return menuItem;
  }

  private Hashtable _subItems = new Hashtable();

  private void CleanSubItems()
  {
   bool itemsAreHiddenItems = MenuType == ConfiguretMenuItemType.HiddenItems;
   bool itemExists, itemIsHidden, itemIsExcluded;

   object[] keys = new object[_subItems.Count];
   _subItems.Keys.CopyTo(keys, 0);

   int firstItemHidden = _navigationPane.GetFirstItemIndex(NavigationPaneItemDisplayType.Undefined);

   for (int j = keys.Length - 1; j > -1; j--)
   {
    object key = keys[j];

    object item = key is NavigationPaneItem ? key : _navigationPane.ItemContainerGenerator.ItemFromContainer(key as DependencyObject);
    itemExists = _navigationPane.Items.Contains(item);
    itemIsExcluded = NavigationPane.GetIsItemExcluded(key as DependencyObject);
    itemIsHidden = _navigationPane.Items.IndexOf(item) >= firstItemHidden && firstItemHidden != -1;

    if (!itemExists || ((!itemIsHidden || itemIsExcluded) && itemsAreHiddenItems))
    {
     if (itemsAreHiddenItems)
      _contextMenu.Items.Remove(_subItems[key]);
     else if (MenuType == ConfiguretMenuItemType.AddRemove)
      Items.Remove(_subItems[key]);
     _subItems.Remove(key);
    }
   }
  }

  private void CheckAddRemoveItems()
  {
   if (_navigationPane == null)
    return;

   foreach (object obj in _navigationPane.Items)
   {
    NavigationPaneItem item = obj as NavigationPaneItem;
    if(item == null)
     item = _navigationPane.ItemContainerGenerator.ContainerFromItem(obj) as NavigationPaneItem;

    if (item == null)
     continue;

    if (!_subItems.Contains(item))
    {
     MenuItem menuItem = MenuItemFromNavigationPaneItem(item, false);
     _subItems.Add(item, menuItem);
     Items.Add(menuItem);
    }
   }
   CleanSubItems();
  }

  private object _separatorTop, _separatorBottom;

  private void CheckHiddenItems()
  {
   if(_contextMenu == null)
    return;

   int startIndex = _contextMenu.Items.IndexOf(this);
   int index = startIndex - _subItems.Count;

   for (int firstHidden = _navigationPane.GetFirstItemIndex(NavigationPaneItemDisplayType.Undefined);
    firstHidden != -1 && firstHidden < _navigationPane.Items.Count; firstHidden++)
   {
    NavigationPaneItem item = _navigationPane.Items[firstHidden] as NavigationPaneItem;
    if (item != null && !NavigationPane.GetIsItemExcluded(item))
    {
     index++;
     if (!_subItems.Contains(item))
     {
      MenuItem menuItem = MenuItemFromNavigationPaneItem(item, true);
      _subItems.Add(item, menuItem);
      _contextMenu.Items.Insert(index, menuItem);
     }
    }
   }
   CleanSubItems();

   bool addSeparator = (_subItems.Count > 0);
   if (addSeparator && Separator == ConfigureMenuSeparator.Top || Separator == ConfigureMenuSeparator.Both)
   {
    if (_separatorTop == null)
     _separatorTop = new Separator();
    if(!_contextMenu.Items.Contains(_separatorTop))
     _contextMenu.Items.Insert(startIndex, _separatorTop);
   }
   else if (_contextMenu.Items.Contains(_separatorTop))
    _contextMenu.Items.Remove(_separatorTop);

   if (addSeparator && Separator == ConfigureMenuSeparator.Bottom || Separator == ConfigureMenuSeparator.Both)
   {
    if (_separatorBottom == null)
     _separatorBottom = new Separator();
    if (!_contextMenu.Items.Contains(_separatorBottom))
     _contextMenu.Items.Insert(index + 1, _separatorBottom);
   }
   else if (_contextMenu.Items.Contains(_separatorBottom))
    _contextMenu.Items.Remove(_separatorBottom);
  }

  void _contextMenu_Opened(object sender, RoutedEventArgs e)
  {
   if (MenuType == ConfiguretMenuItemType.AddRemove)
    CheckAddRemoveItems();
   else if (MenuType == ConfiguretMenuItemType.HiddenItems)
    CheckHiddenItems();
  }

  private ContextMenu _contextMenu;
  private NavigationPane _navigationPane;

  private object TryFindNavigationPane()
  {
   DependencyObject v = this;
   do
   {
    v = LogicalTreeHelper.GetParent(v);
   } while (v != null && !(v is ContextMenu));

   if (v != null)
   {
    _contextMenu = v as ContextMenu;
    if (_contextMenu.PlacementTarget is NavigationPaneItem)
    {
     return (_contextMenu.PlacementTarget as NavigationPaneItem).navigationPane;
    }
    return (_contextMenu.PlacementTarget as FrameworkElement).TemplatedParent as NavigationPane;
   }
   return v;
  }

  protected override void OnVisualParentChanged(DependencyObject oldParent)
  {
   base.OnVisualParentChanged(oldParent);

   _navigationPane = TryFindNavigationPane() as NavigationPane;
   if (MenuType != ConfiguretMenuItemType.MenuItem)
    SetValues();  
  }

  public override void OnApplyTemplate()
  {
   if(CorrectDefaultStyle)
   {
    // in the case an external override has be made for the MeneItem
    // make sure this menu item gets the correct overriden style !
    if(Style == null)
     Style = new Style(GetType(), this.FindResource(typeof(System.Windows.Controls.MenuItem)) as Style);
   }

   base.OnApplyTemplate();
  }
 }
}
