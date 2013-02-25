#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Stema.Controls.Utils;
using System.Collections.ObjectModel;

namespace Stema.Controls
{
 /// <summary>
 /// </summary>
 public class NavigationPaneItem : HeaderedContentControl, ICommandSource
 {
  static NavigationPaneItem()
  {
   DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPaneItem), new FrameworkPropertyMetadata(typeof(NavigationPaneItem)));
  }

  internal NavigationPane navigationPane
  {
   get
   {
    return (ItemsControl.ItemsControlFromItemContainer(this) as NavigationPane);
   }
  }

  #region DependecyPropeties

  /// <summary>
  /// True if we are inside a NavigationPane
  /// </summary>
  public bool IsInsideNavigationPane
  {
   get { return (bool)GetValue(IsInsideNavigationPaneProperty); }
   protected set { SetValue(IsInsideNavigationPanePropertyKey, value); }
  }
  private static readonly DependencyPropertyKey IsInsideNavigationPanePropertyKey =
      DependencyProperty.RegisterReadOnly("IsInsideNavigationPane", typeof(bool), typeof(NavigationPaneItem),
      new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
  public static readonly DependencyProperty IsInsideNavigationPaneProperty = IsInsideNavigationPanePropertyKey.DependencyProperty;

  protected override void OnVisualParentChanged(DependencyObject oldParent)
  {
   base.OnVisualParentChanged(oldParent);
   IsInsideNavigationPane = VisualParent is NavigationPanePanel || VisualParent is NavigationPane;
  }

  #region Images

  /// <summary>
  /// The image displayed as icon when item is displayed as LargeItem
  /// </summary>
  public ImageSource Image
  {
   get { return (ImageSource)GetValue(ImageProperty); }
   set { SetValue(ImageProperty, value); }
  }
  public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource),
   typeof(NavigationPaneItem), new FrameworkPropertyMetadata(null, null, new CoerceValueCallback(CoerceImage)));

  private static object CoerceImage(DependencyObject d, object value)
  {
   NavigationPaneItem item = d as NavigationPaneItem;
   item.CoerceValue(ImageSmallProperty);
   if (item.ImageSmall == null)
    item.ImageSmall = (ImageSource)value;

   return value;
  }

  /// <summary>
  /// The image tath will be used in SmallItems box. If left null the Image property will be used
  /// </summary>
  public ImageSource ImageSmall
  {
   get { return (ImageSource)GetValue(ImageSmallProperty); }
   set { SetValue(ImageSmallProperty, value); }
  }
  public static readonly DependencyProperty ImageSmallProperty = DependencyProperty.Register("ImageSmall", typeof(ImageSource),
   typeof(NavigationPaneItem), new FrameworkPropertyMetadata(null));

  #endregion

  #endregion

  #region SUBITEMS

  /// <summary>
  /// SubItems are buttons displayd under the SelectedItem Bar Title when the NavigationPane is minimized
  /// </summary>
  public ObservableCollection<NavigationPaneButton> SubItems
  {
   get { return (ObservableCollection<NavigationPaneButton>)GetValue(SubItemsProperty); }
   set { SetValue(SubItemsProperty, value); }
  }
  public static readonly DependencyProperty SubItemsProperty =
      DependencyProperty.Register("SubItems", typeof(ObservableCollection<NavigationPaneButton>), typeof(NavigationPaneItem));

  /// <summary>
  /// Indicates if the items has SubItems
  /// </summary>
  public bool HasSubItems
  {
   get { return (bool)GetValue(HasSubItemsProperty); }
  }
  public static readonly DependencyPropertyKey HasSubItemsPropertyKey =
      DependencyProperty.RegisterReadOnly("HasSubItems", typeof(bool), typeof(NavigationPaneItem),
      new UIPropertyMetadata(false, null, CoerceHasSubItems));
  public static readonly DependencyProperty HasSubItemsProperty = HasSubItemsPropertyKey.DependencyProperty;

  private static object CoerceHasSubItems(DependencyObject d, object value)
  {
   NavigationPaneItem item = d as NavigationPaneItem;
   return item.SubItems.Count > 0;
  }

  public override void BeginInit()
  {
   SubItems = new ObservableCollection<NavigationPaneButton>();
   SubItems.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SubItems_CollectionChanged);
   base.BeginInit();
  }

  public override void OnApplyTemplate()
  {
   base.OnApplyTemplate();
  }

  void SubItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
  {
   CoerceValue(SubItemsProperty);
  }

  #endregion

  protected override void OnHeaderChanged(object oldHeader, object newHeader)
  {
   base.OnHeaderChanged(oldHeader, newHeader);
   CoerceValue(ButtonToolTipGestureProperty);
  }

  protected override void OnContentChanged(object oldContent, object newContent)
  {
   base.OnContentChanged(oldContent, newContent);
   if (IsSelected && (navigationPane != null))
    navigationPane.SelectedContent = newContent;
  }

  public bool FocusContent()
  {
   if (!base.IsKeyboardFocusWithin && (base.Content is UIElement))
   {
    base.UpdateLayout();
    ((UIElement)base.Content).MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
   }
   return true;
  }

  public bool IsExcluded
  {
   get { return NavigationPane.GetIsItemExcluded(this); }
   set { NavigationPane.SetIsItemExcluded(this, value); }
  }

  public bool IsNotExcluded
  {
   get { return !IsExcluded; }
   set { IsExcluded = !value; }
  }
  /*
  public bool CanResize
  {
   get { return (bool)GetValue(CanResizeProperty); }
   set { SetValue(CanResizeProperty, value); }
  }
  public static readonly DependencyProperty CanResizeProperty =
    NavigationPaneExpander.CanResizeProperty.AddOwner(typeof(NavigationPaneItem));
  */
  #region SELECTION

  /// <summary>
  /// Indicate wether the item is the current item selected in the navigationpane
  /// </summary>
  public bool IsSelected
  {
   get { return (bool)GetValue(IsSelectedProperty); }
   set { SetValue(IsSelectedProperty, value); }
  }
  public static readonly DependencyProperty IsSelectedProperty =
    Selector.IsSelectedProperty.AddOwner(typeof(NavigationPaneItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Journal | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsParentMeasure, new PropertyChangedCallback(OnIsSelectedChanged)));

  private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   NavigationPaneItem item = d as NavigationPaneItem;
    
   if ((bool)e.NewValue)
   {
    NavigationPane nav = item.navigationPane; // item.Parent as NavigationPane;
    if (nav != null)
    {
     NavigationPaneItem selItem = (nav.SelectedItem is NavigationPaneItem ? nav.SelectedItem :
      nav.ItemContainerGenerator.ContainerFromItem(nav.SelectedItem)) as NavigationPaneItem;
     if (selItem != null && selItem != item)
       (selItem as NavigationPaneItem).IsSelected = false;
    }
    item.ChangeSelected(true);
   }
   else
    item.ChangeSelected(false);
  }

  private void ChangeSelected(bool selected)
  {
   RoutedEventArgs e = null;
   if (selected)
   {
    e = new RoutedEventArgs(Selector.SelectedEvent, this);
    RaiseEvent(e);
    OnSelected(e);

    if (this.Command != null)
    {
     RoutedCommand command = Command as RoutedCommand;

     if (command != null)
      command.Execute(CommandParameter, CommandTarget);
     else
      ((ICommand)Command).Execute(CommandParameter);
    }
   }
   else
   {
    e = new RoutedEventArgs(Selector.UnselectedEvent, this);
    RaiseEvent(e);
    OnUnselected(e);
   }
  }

  protected virtual void OnUnselected(RoutedEventArgs e)
  {
  }

  protected virtual void OnSelected(RoutedEventArgs e)
  {
  }

  protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
  {
   base.OnGotKeyboardFocus(e);
   if ((!e.Handled && (e.NewFocus == this)) && !IsSelected)
    IsSelected = true;
  }

  #endregion

  public string Gesture
  {
   get { return (string)GetValue(GestureProperty); }
   internal set { SetValue(GesturePropertyKey, value); }
  }

  // Using a DependencyProperty as the backing store for Gesture.  This enables animation, styling, binding, etc...
  private static readonly DependencyPropertyKey GesturePropertyKey =
      DependencyProperty.RegisterReadOnly("Gesture", typeof(string), typeof(NavigationPaneItem), new UIPropertyMetadata(null, ToolTipGestureChanged));
  public static readonly DependencyProperty GestureProperty = GesturePropertyKey.DependencyProperty;

  public object ButtonToolTip
  {
   get { return (object)GetValue(ButtonToolTipProperty); }
   set { SetValue(ButtonToolTipProperty, value); }
  }
  public static readonly DependencyProperty ButtonToolTipProperty = DependencyProperty.Register("ButtonToolTip", typeof(object), typeof(NavigationPaneItem),
   new UIPropertyMetadata(null, ToolTipGestureChanged));

  public bool ShowGesture
  {
   get { return (bool)GetValue(ShowGestureProperty); }
   set { SetValue(ShowGestureProperty, value); }
  }
  public static readonly DependencyProperty ShowGestureProperty =
      DependencyProperty.Register("ShowGesture", typeof(bool), typeof(NavigationPaneItem), new UIPropertyMetadata(true, ToolTipGestureChanged));

  private static void ToolTipGestureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   d.CoerceValue(ButtonToolTipGestureProperty);
  }

  public object ButtonToolTipGesture
  {
   get { return (object)GetValue(ButtonToolTipGestureProperty); }
  }
  public static readonly DependencyPropertyKey ButtonToolTipGestureKey =
      DependencyProperty.RegisterReadOnly("ButtonToolTipGesture", typeof(object), typeof(NavigationPaneItem), new UIPropertyMetadata(null, null, CoerceButtonToolTip));
  public static readonly DependencyProperty ButtonToolTipGestureProperty = ButtonToolTipGestureKey.DependencyProperty;
  private static object CoerceButtonToolTip(DependencyObject d, object value)
  {
   NavigationPaneItem item = d as NavigationPaneItem;
   object toolTip = item.ButtonToolTip == null ? item.Header : item.ButtonToolTip;

   if(toolTip is string && item.ShowGesture)
    return string.Format("{0} ({1})", toolTip, item.Gesture);
   else
    return toolTip;
  }
  
  internal KeyBinding keyBinding;

  // Make Command a dependency property so it can use databinding.
  public static readonly DependencyProperty CommandProperty =
      DependencyProperty.Register("Command", typeof(ICommand), typeof(NavigationPaneItem), new PropertyMetadata((ICommand)null, new PropertyChangedCallback(CommandChanged)));

  public ICommand Command
  {
   get
   {
    return (ICommand)GetValue(CommandProperty);
   }
   set
   {
    SetValue(CommandProperty, value);
   }
  }
  // Command dependency property change callback.
  private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   NavigationPaneItem item = (NavigationPaneItem)d;
   item.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
  }

  // Add a new command to the Command Property.
  private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
  {
   // If oldCommand is not null, then we need to remove the handlers.
   if (oldCommand != null)
    RemoveCommand(oldCommand, newCommand);
   AddCommand(oldCommand, newCommand);
  }

  // Remove an old command from the Command Property.
  private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
  {
   EventHandler handler = CanExecuteChanged;
   oldCommand.CanExecuteChanged -= handler;
  }

  // Add the command.
  private void AddCommand(ICommand oldCommand, ICommand newCommand)
  {
   EventHandler handler = new EventHandler(CanExecuteChanged);
   if (newCommand != null)
    newCommand.CanExecuteChanged += handler;
  }

  private void CanExecuteChanged(object sender, EventArgs e)
  {

   if (this.Command != null)
   {
    RoutedCommand command = this.Command as RoutedCommand;

    // If a RoutedCommand.
    if (command != null)
     IsEnabled = command.CanExecute(CommandParameter, CommandTarget);
    // If a not RoutedCommand.
    else
     IsEnabled = Command.CanExecute(CommandParameter);
   }
  }

  public object CommandParameter
  {
   get { return (object)GetValue(CommandParameterProperty); }
   set { SetValue(CommandParameterProperty, value); }
  }
  // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
  public static readonly DependencyProperty CommandParameterProperty =
      DependencyProperty.Register("CommandParameter", typeof(object), typeof(NavigationPaneItem), new UIPropertyMetadata(null));

  public IInputElement CommandTarget
  {
   get { return (IInputElement)GetValue(CommandTargetProperty); }
   set { SetValue(CommandTargetProperty, value); }
  }
  // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
  public static readonly DependencyProperty CommandTargetProperty =
      DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(NavigationPaneItem), new UIPropertyMetadata(null));
 }
}
