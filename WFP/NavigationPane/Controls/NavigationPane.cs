#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using NavigationPane.Properties;
using Stema.Controls.Utils;
using Stema.Utils;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Interop;
using System.Windows.Media;
using System.Threading;

namespace Stema.Controls
{
	/// <summary>
	/// </summary>
	[
		StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationPaneItem)),
		TemplatePart(Name = NavigationPaneExpander.NavigationPaneExpanderParts.PART_ContentHost, Type = typeof(FrameworkElement)),
		TemplatePart(Name = NavigationPaneParts.PART_ConfigureMenuButton, Type = typeof(NavigationPaneButton)),
		TemplatePart(Name = NavigationPaneExpander.NavigationPaneExpanderParts.PART_ResizeThumb, Type = typeof(Thumb)),
		TemplatePart(Name = NavigationPaneParts.PART_PaneMininizedSizeProvider, Type = typeof(FrameworkElement))
	]
	public class NavigationPane : Selector, IMinimizeHelped, ICommandSource
	{
		internal static class NavigationPaneParts
		{
			internal const string PART_ConfigureMenuButton = "PART_ConfigureMenuButton";
			internal const string PART_PaneMininizedSizeProvider = "PART_PaneMininizedSizeProvider";
		}

		static NavigationPane()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPane), new FrameworkPropertyMetadata(typeof(NavigationPane)));

			CommandManager.RegisterClassCommandBinding(typeof(NavigationPane),
				new CommandBinding(ConfigureCommand, new ExecutedRoutedEventHandler(Command_Executed),
					new CanExecuteRoutedEventHandler(Command_CanExecute)));
			CommandManager.RegisterClassCommandBinding(typeof(NavigationPane),
				new CommandBinding(SelectItemCommand, new ExecutedRoutedEventHandler(Command_Executed),
					new CanExecuteRoutedEventHandler(Command_CanExecute)));
		}

		#region Commands

		private const string SelectItemCommandName = "NavgationPane_SelectItemCommand";
		private const string ConfigureCommandName = "NavgationPane_ConfigureCommand";
		public static RoutedCommand ConfigureCommand = new RoutedCommand(ConfigureCommandName, typeof(NavigationPane));
		public static RoutedCommand SelectItemCommand = new RoutedCommand(SelectItemCommandName, typeof(NavigationPane));

		private static object FindRealCommandSource(object sender, RoutedEventArgs e)
		{
			if (sender is NavigationPane || sender is NavigationPaneItem)
				return sender;
			if (e.Source is NavigationPane || e.Source is NavigationPaneItem)
				return e.Source;
			if (e.OriginalSource is NavigationPane)
				return e.OriginalSource;

			if ((e.Source is ContextMenu) || ((sender as ContextMenu).PlacementTarget is NavigationPane) || ((sender as ContextMenu).PlacementTarget is NavigationPaneItem))
				return (sender as ContextMenu).PlacementTarget;

			return null;
		}

		public static void Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (e.Command == ConfigureCommand)
			{
				object o = FindRealCommandSource(sender, e);
				if (o != null)
				{
					NavigationPane p = o is NavigationPane ? o as NavigationPane : (o as NavigationPaneItem).navigationPane;

					string parameter = e.Parameter != null ? e.Parameter.ToString().ToLower() : null;
					switch (parameter)
					{
						case "more":
							e.CanExecute = p.LargeItems < p.Items.Count - p.ExcludedItemsCount;
							break;
						case "fewer":
							e.CanExecute = p.LargeItems > 0 && p.Items.Count - p.ExcludedItemsCount > 0;
							break;
						case "toggleExcluded":
							e.CanExecute = e.Parameter is NavigationPaneItem;
							break;
						default:
							e.CanExecute = true;
							break;
					}
				}
			}
			else
			{
				// SelectItemCommand
				e.CanExecute = e.Parameter is NavigationPaneItem;
			}
		}

		public static void Command_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == ConfigureCommand)
			{
				object o = FindRealCommandSource(sender, e);
				if (o != null)
				{
					NavigationPane p = o is NavigationPane ? o as NavigationPane : (o as NavigationPaneItem).navigationPane;

					string parameter;
					if (e.Parameter is NavigationPaneItem)
						parameter = "toggleExcluded";
					else
						parameter = e.Parameter != null ? e.Parameter.ToString().ToLower() : null;
					switch (parameter)
					{
						case "more":
							p.LargeItems++;
							break;
						case "fewer":
							p.LargeItems--;
							break;
						case "toggleExcluded":
							NavigationPaneItem item = e.Parameter as NavigationPaneItem;
       if(item != null)
							 NavigationPane.SetIsItemExcluded(item, !NavigationPane.GetIsItemExcluded(item));
							//if (item.navigationPane != null && item.navigationPane.ConfigureMenu != null)
							//{
							// (item.navigationPane.ConfigureMenu.Items.CurrentItem as MenuItem).InvalidateProperty(MenuItem.IsCheckedProperty);
							//}
							break;
						case "options":
							p.ShowOptions();
							break;
					}
				}
			}
			else
			{
				(e.Parameter as NavigationPaneItem).IsSelected = true;
			}
		}

		private void ShowOptions()
		{
			if (RaiseConfigureWindowOpenedEvent())
			{
				NavigationPaneOptions o = new NavigationPaneOptions(this);
				o.Owner = Window.GetWindow(this);
				bool? result = o.ShowDialog();
				if (result.HasValue && result.Value == true)
				{
					if (o.CanReorder)
					{
						DependencyObject[] items = new DependencyObject[Items.Count];
						Items.CopyTo(items, 0);
						Items.Clear();
					}

					ObservableCollection<NavigationPaneOptions.NavigationPaneOptionsData> data = o.Items;
					foreach (NavigationPaneOptions.NavigationPaneOptionsData itemData in data)
					{
						DependencyObject item = itemData.item as DependencyObject;
						if (o.CanReorder)
							Items.Add(item);
						SetIsItemExcluded(item as DependencyObject, !itemData.IsNotExcluded);
					}
					InvalidateMeasure();
				}
			}
		}

		#endregion Commands

		#region SubItems

		public ObservableCollection<NavigationPaneButton> SelectedItemSubItems
		{
			get { return (ObservableCollection<NavigationPaneButton>)GetValue(SelectedItemSubItemsProperty); }
		}
		public static readonly DependencyPropertyKey SelectedItemSubItemsPropertyKey =
						DependencyProperty.RegisterReadOnly("SelectedItemSubItems", typeof(ObservableCollection<NavigationPaneButton>), typeof(NavigationPane), new PropertyMetadata(null, null, CoerceSelectedItemSubItems));
		public static readonly DependencyProperty SelectedItemSubItemsProperty = SelectedItemSubItemsPropertyKey.DependencyProperty;

		private static object CoerceSelectedItemSubItems(DependencyObject d, object value)
		{
			NavigationPane n = d as NavigationPane;
			NavigationPaneItem item = n.SelectedItem as NavigationPaneItem;
			if (item != null)
				return (n.SelectedItem as NavigationPaneItem).SubItems;

			return null;
		}

		#endregion

		public object BarTitle
		{
			get { return (object)GetValue(BarTitleProperty); }
			set { SetValue(BarTitleProperty, value); }
		}
		public static readonly DependencyProperty BarTitleProperty =
						DependencyProperty.Register("BarTitle", typeof(object), typeof(NavigationPane), new UIPropertyMetadata(
							NPR.NavigationPane_BarTitle));

		public object BarToolTip
		{
			get { return (object)GetValue(BarToolTipProperty); }
			set { SetValue(BarToolTipProperty, value); }
		}
		public static readonly DependencyProperty BarToolTipProperty =
			//NavigationPaneExpander.BarToolTipProperty(typeof(NavigationPane), new UIPropertyMetadata(NPR.NavigationPane_BarTooltip));
						DependencyProperty.Register("BarToolTip", typeof(object), typeof(NavigationPane), new UIPropertyMetadata(NPR.NavigationPane_BarTooltip));

		public int LargeItems
		{
			get { return (int)GetValue(LargeItemsProperty); }
			set { SetValue(LargeItemsProperty, value); }
		}
		public static readonly DependencyProperty LargeItemsProperty =
						DependencyProperty.Register("LargeItems", typeof(int), typeof(NavigationPane),
						new FrameworkPropertyMetadata(3, new PropertyChangedCallback(OnVisibleItemsChanged)));

		public int SmallItems
		{
			get { return (int)GetValue(SmallItemsKey.DependencyProperty); }
			internal set { SetValue(SmallItemsKey, value); }
		}
		public static readonly DependencyPropertyKey SmallItemsKey =
						DependencyProperty.RegisterReadOnly("SmallItems", typeof(int), typeof(NavigationPane), new UIPropertyMetadata(0, new PropertyChangedCallback(OnVisibleItemsChanged)));

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedContent
		{
			get { return base.GetValue(SelectedContentProperty); }
			internal set { base.SetValue(SelectedContentPropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedContentPropertyKey = DependencyProperty.RegisterReadOnly("SelectedContent", typeof(object), typeof(NavigationPane), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedContentProperty = SelectedContentPropertyKey.DependencyProperty;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedHeader
		{
			get { return base.GetValue(SelectedHeaderProperty); }
			internal set { base.SetValue(SelectedHeaderPropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedHeaderPropertyKey = DependencyProperty.RegisterReadOnly("SelectedHeader", typeof(object), typeof(NavigationPane), 
			new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedHeaderProperty = SelectedHeaderPropertyKey.DependencyProperty;

		internal int ExcludedItemsCount;
		public static bool GetIsItemExcluded(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsItemExcludedProperty);
		}
		public static void SetIsItemExcluded(DependencyObject obj, bool value)
		{
			obj.SetValue(IsItemExcludedProperty, value);
		}
		public static readonly DependencyProperty IsItemExcludedProperty =
						DependencyProperty.RegisterAttached("IsItemExcluded", typeof(bool), typeof(NavigationPane),
						new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsItemExcludedChanged)));

		private static void OnIsItemExcludedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NavigationPaneItem item = d as NavigationPaneItem;
   if (item != null)
   {
    NavigationPane navigationPane = item.navigationPane;
    if ((bool)e.NewValue == true && GetIsSelected(item))
    {
     if (navigationPane.Items.Count == 1)
     {
      navigationPane.SelectedIndex = -1;
     }
     else
     {
      // try to find the previous item
      NavigationPane np = item.navigationPane;
      NavigationPaneItem item2;

      int index = np.ItemContainerGenerator.IndexFromContainer(item);// np.Items.IndexOf(item);
      int resIndex = index;
      while (--resIndex > -1)
      {
       //item2 = np.Items[resIndex] as NavigationPaneItem;
       item2 = np.ItemContainerGenerator.ContainerFromIndex(resIndex) as NavigationPaneItem;
       if (!NavigationPane.GetIsItemExcluded(item2))
       {
        item2.IsSelected = true;
        break;
       }
      }
      if (resIndex == -1)
      {
       while (++index < np.Items.Count)
       {
        item2 = np.ItemContainerGenerator.ContainerFromIndex(index) as NavigationPaneItem;
        if (!NavigationPane.GetIsItemExcluded(item2))
        {
         item2.IsSelected = true;
         break;
        }
       }
      }
     }
    }
    else
    {
     if (navigationPane.Items.Count == 1)
     {
      item.IsSelected = true;
     }
    }
   }
			OnVisibleItemsChanged(d, e);
		}

		public bool IsSmallItemsVisible
		{
			get { return (bool)GetValue(IsSmallItemsVisibleProperty); }
			set { SetValue(IsSmallItemsVisibleProperty, value); }
		}
		public static readonly DependencyProperty IsSmallItemsVisibleProperty =
						DependencyProperty.Register("IsSmallItemsVisible", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true, new PropertyChangedCallback(OnVisibleItemsChanged)));

		private static void OnVisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NavigationPane p = d as NavigationPane;
			if ((p == null) && d is NavigationPaneItem)
				p = (d as NavigationPaneItem).navigationPane;
			if (p == null)
				p = (d as FrameworkElement).Parent as NavigationPane;

			if (p != null)
			{
				if (e.Property == IsItemExcludedProperty)
					p.ExcludedItemsCount += (bool)e.NewValue ? 1 : -1;

				p.RaiseVisibleItemsChangedEvent();
			}
		}

		public event RoutedEventHandler VisibleItemsChanged
		{
			add { AddHandler(VisibleItemsChangedEvent, value); }
			remove { RemoveHandler(VisibleItemsChangedEvent, value); }
		}
		public static readonly RoutedEvent VisibleItemsChangedEvent = EventManager.RegisterRoutedEvent(
						"VisibleItemsChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPane));

		private void RaiseVisibleItemsChangedEvent()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(VisibleItemsChangedEvent);
			RaiseEvent(newEventArgs);
		}

		internal int GetLastItemIndex(NavigationPaneItemDisplayType type)
		{
			int index = -1;
			if (type == NavigationPaneItemDisplayType.Large)
			{
				index = GetFirstItemIndex(NavigationPaneItemDisplayType.Small);
				if (index == -1)
					index = GetFirstItemIndex(NavigationPaneItemDisplayType.Undefined);
			}
			else if (type == NavigationPaneItemDisplayType.Small)
				index = GetFirstItemIndex(NavigationPaneItemDisplayType.Undefined);

			if (index == -1)
				index = Items.Count;

			while (index-- > 0)
			{
				DependencyObject item = Items[index] as NavigationPaneItem;
				if(item == null)
					item = ItemContainerGenerator.ContainerFromIndex(index);
				if (!GetIsItemExcluded(item))
					break;
			}
			return index;
		}

		internal int GetFirstItemIndex(NavigationPaneItemDisplayType type)
		{
			int reach = 0;
			if (type != NavigationPaneItemDisplayType.Large)
				reach += LargeItems;
			if (type == NavigationPaneItemDisplayType.Undefined && IsSmallItemsVisible)
				reach += SmallItems;

			int index = 0;
			int count = 0;
			for (; index < Items.Count && count < reach; index++)
			{
				NavigationPaneItem item = Items[index] as NavigationPaneItem;
				if (item == null)
					item = ItemContainerGenerator.ContainerFromIndex(index) as NavigationPaneItem;
				if (item != null && !GetIsItemExcluded(item))
					count++;
			}
			if (index == Items.Count)
				index = -1;
			return index;
		}

		#region Templates

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTemplate SelectedTitleTemplate
		{
			get { return (DataTemplate)base.GetValue(SelectedTitleTemplateProperty); }
			internal set { base.SetValue(SelectedTitleTemplatePropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedTitleTemplatePropertyKey = DependencyProperty.RegisterReadOnly("SelectedTitleTemplate", typeof(DataTemplate), typeof(NavigationPane), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedTitleTemplateProperty = SelectedTitleTemplatePropertyKey.DependencyProperty;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTemplate SelectedContentTemplate
		{
			get { return (DataTemplate)base.GetValue(SelectedContentTemplateProperty); }
			internal set { base.SetValue(SelectedContentTemplatePropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedContentTemplatePropertyKey = DependencyProperty.RegisterReadOnly("SelectedContentTemplate", typeof(DataTemplate), typeof(NavigationPane), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedContentTemplateProperty = SelectedContentTemplatePropertyKey.DependencyProperty;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTemplateSelector SelectedContentTemplateSelector
		{
			get { return (DataTemplateSelector)base.GetValue(SelectedContentTemplateSelectorProperty); }
			internal set { base.SetValue(SelectedContentTemplateSelectorPropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedContentTemplateSelectorPropertyKey = DependencyProperty.RegisterReadOnly("SelectedContentTemplateSelector", typeof(DataTemplateSelector), typeof(NavigationPane), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedContentTemplateSelectorProperty = SelectedContentTemplateSelectorPropertyKey.DependencyProperty;

		public string SelectedContentStringFormat
		{
			get { return (string)base.GetValue(SelectedContentStringFormatProperty); }
			internal set { base.SetValue(SelectedContentStringFormatPropertyKey, value); }
		}
		private static readonly DependencyPropertyKey SelectedContentStringFormatPropertyKey = DependencyProperty.RegisterReadOnly("SelectedContentStringFormat", typeof(string), typeof(NavigationPane), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty SelectedContentStringFormatProperty = SelectedContentStringFormatPropertyKey.DependencyProperty;

		public DataTemplate ContentTemplate
		{
			get { return (DataTemplate)base.GetValue(ContentTemplateProperty); }
			set { base.SetValue(ContentTemplateProperty, value); }
		}
		public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(NavigationPane), new FrameworkPropertyMetadata(null));

		public DataTemplateSelector ContentTemplateSelector
		{
			get { return (DataTemplateSelector)base.GetValue(ContentTemplateSelectorProperty); }
			set { base.SetValue(ContentTemplateSelectorProperty, value); }
		}
		public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register("ContentTemplateSelector", typeof(DataTemplateSelector), typeof(NavigationPane), new FrameworkPropertyMetadata(null));

		public string ContentStringFormat
		{
			get { return (string)base.GetValue(ContentStringFormatProperty); }
			set { base.SetValue(ContentStringFormatProperty, value); }
		}
		public static readonly DependencyProperty ContentStringFormatProperty = DependencyProperty.Register("ContentStringFormat", typeof(string), typeof(NavigationPane), new FrameworkPropertyMetadata(null));

		#endregion

		#region Collapsing

		public bool IsPopupOpen
		{
			get { return (bool)GetValue(IsPopupOpenProperty); }
			set { SetValue(IsPopupOpenProperty, value); }
		}
		public static readonly DependencyProperty IsPopupOpenProperty =
						DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(false, null, new CoerceValueCallback(CoerceIsPopupOpen)));

		private static object CoerceIsPopupOpen(DependencyObject d, object value)
		{
			NavigationPane p = d as NavigationPane;
			if (p.IsMinimized)
				return false;
			return value;
		}

		public object ExpandToolTip
		{
			get { return (object)GetValue(ExpandToolTipProperty); }
			set { SetValue(ExpandToolTipProperty, value); }
		}
		public static readonly DependencyProperty ExpandToolTipProperty =
			DependencyProperty.Register("ExpandToolTip", typeof(object), typeof(NavigationPane),
						new UIPropertyMetadata(NPR.NavigationPane_ExpandToolTip, MinimizeHelper.ButtonToolTipChanged));

		public object MinimizeToolTip
		{
			get { return (object)GetValue(MinimizeToolTipProperty); }
			set { SetValue(MinimizeToolTipProperty, value); }
		}
		public static readonly DependencyProperty MinimizeToolTipProperty =
						DependencyProperty.Register("MinimizeToolTip", typeof(object), typeof(NavigationPane),
						new UIPropertyMetadata(NPR.NavigationPane_MinimizeToolTip, MinimizeHelper.ButtonToolTipChanged));

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ButtonToolTip
		{
			get { return (object)GetValue(ButtonToolTipProperty); }
		}
		private static readonly DependencyPropertyKey ButtonToolTipPropertyKey = 
			DependencyProperty.RegisterReadOnly("ButtonToolTip", typeof(object), typeof(NavigationPane), 
			new UIPropertyMetadata(NPR.NavigationPane_MinimizeToolTip, null, MinimizeHelper.CoerceButtonToolTip));
		public static readonly DependencyProperty ButtonToolTipProperty = ButtonToolTipPropertyKey.DependencyProperty;

		private double MinimizedMinWidth
		{
			get { return (double)GetValue(MinimizedMinWidthProperty); }
			set { SetValue(MinimizedMinWidthProperty, value); }
		}
		internal static readonly DependencyProperty MinimizedMinWidthProperty =
			DependencyProperty.Register("MinimizedMinWidth", typeof(double), typeof(NavigationPane),
			new PropertyMetadata(0.0, null, new CoerceValueCallback(MinimizeHelper.CoerceMinimizedMinWidth)));
		
		public bool HasResizeThumb
		{
			get { return (bool)GetValue(HasResizeThumbProperty); }
			set { SetValue(HasResizeThumbProperty, value); }
		}
		public static readonly DependencyProperty HasResizeThumbProperty =
			DependencyProperty.Register("HasResizeThumb", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));

		public bool IsMinimizeButtonVisible
		{
			get { return (bool)GetValue(IsMinimizeButtonVisibleProperty); }
			set { SetValue(IsMinimizeButtonVisibleProperty, value); }
		}
		public static readonly DependencyProperty IsMinimizeButtonVisibleProperty =
						DependencyProperty.Register("IsMinimizeButtonVisible", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));

		public bool CanMinimize
		{
			get { return (bool)GetValue(CanMinimizeProperty); }
			set { SetValue(CanMinimizeProperty, value); }
		}
		public static readonly DependencyProperty CanMinimizeProperty =
						DependencyProperty.Register("CanMinimize", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));

		public bool IsMinimized
		{
			get { return (bool)GetValue(IsMinimizedProperty); }
			set { SetValue(IsMinimizedProperty, value); }
		}
		public static readonly DependencyProperty IsMinimizedProperty =
			DependencyProperty.Register("IsMinimized", typeof(bool), typeof(NavigationPane),
						new FrameworkPropertyMetadata(false,
							FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
							new PropertyChangedCallback(MinimizeHelper.OnIsMinimizedChanged), new CoerceValueCallback(MinimizeHelper.CoerceIsMinimized)));

		#endregion

		public bool IsSplitterEnabled
		{
			get { return (bool)GetValue(IsSplitterEnabledProperty); }
			set { SetValue(IsSplitterEnabledProperty, value); }
		}
		public static readonly DependencyProperty IsSplitterEnabledProperty =
						DependencyProperty.Register("IsSplitterEnabled", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));
		
		public bool HasSplitter
		{
			get { return (bool)GetValue(HasSplitterProperty); }
			set { SetValue(HasSplitterProperty, value); }
		}
		public static readonly DependencyProperty HasSplitterProperty =
						DependencyProperty.Register("HasSplitter", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));

		internal NavigationPaneButton part_configureMenuButton { get { return GetTemplateChild(NavigationPaneParts.PART_ConfigureMenuButton) as NavigationPaneButton; } }
		internal FrameworkElement part_PaneMininizedSizeProvider { get { return GetTemplateChild(NavigationPaneParts.PART_PaneMininizedSizeProvider) as FrameworkElement; } }
		internal FrameworkElement part_contentHost;


		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			part_contentHost = GetTemplateChild(NavigationPaneExpander.NavigationPaneExpanderParts.PART_ContentHost) as FrameworkElement;
			NavigationPaneExpander expander = part_contentHost as NavigationPaneExpander;
			if (expander != null)
			{
				expander.ApplyTemplate();
				part_contentHost = expander.Get_PART_ContentHost();
			}

			if (part_contentHost != null)
				part_contentHost.SizeChanged += part_contentHostSizechanged;

			//check minimizeHelper because on blend when you try to edit the style BeginInit() is not already called
			// or is not called at all !! ?? Anyways... check to avoid use of null value reference
			if (minimizeHelper != null)
				minimizeHelper.Init(GetTemplateChild(NavigationPaneExpander.NavigationPaneExpanderParts.PART_ResizeThumb) as Thumb,
				GetTemplateChild(NavigationPaneParts.PART_PaneMininizedSizeProvider) as FrameworkElement, part_contentHost);

			if (part_configureMenuButton != null)
			{
				part_configureMenuButton.Click += new RoutedEventHandler(ConfigureButtonClick);

				// since the order of the events the placement target for the ConfigMenu doesen't get updated
				// and this correct also the fact that even a normal ContextMenu gets the correct PlacementTarhetProperty
				// so.. we manually set it here
				if (ConfigureMenu != null)
				{
					SetConfigmenuPlacementTarget(this, ConfigureMenu, part_configureMenuButton);
					ConfigureMenu.Closed += new RoutedEventHandler(ConfigureMenu_Closed);
				}
			}
		}

		private MinimizeHelper minimizeHelper;
		MinimizeHelper IMinimizeHelped.ResizeHelper
		{
			get { return minimizeHelper; }
		}

		public event RoutedEventHandler MinimizedChanged
		{
			add { AddHandler(MinimizedChangedEvent, value); }
			remove { RemoveHandler(MinimizedChangedEvent, value); }
		}
		public static readonly RoutedEvent MinimizedChangedEvent = EventManager.RegisterRoutedEvent(
						"MinimizedChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPane));

		private void RaiseMinimizedChangedEvent()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(MinimizedChangedEvent);
			RaiseEvent(newEventArgs);
		}

		private double widthDelta;
		public double ExpandedWidth
		{
			get { return (double)GetValue(ExpandedWidthProperty); }
		}
		private static readonly DependencyPropertyKey ExpandedWidthPropertyKey =
						DependencyProperty.RegisterReadOnly("ExpandedWidth", typeof(double), typeof(NavigationPane), new UIPropertyMetadata(0.0, null, CoerceExpandedWidth));
		public static readonly DependencyProperty ExpandedWidthProperty = ExpandedWidthPropertyKey.DependencyProperty;

		private static object CoerceExpandedWidth(DependencyObject d, object value)
		{
			NavigationPane n = d as NavigationPane;
			if (n.IsMinimized)
				return n.widthDelta + n.part_contentHost.ActualWidth;
			else
				return n.ActualWidth;
		}

		private void part_contentHostSizechanged(object Sender, SizeChangedEventArgs e)
		{
			if (IsMinimized)
				CoerceValue(ExpandedWidthProperty);
		}

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if (e.Property == ActualWidthProperty)
				CoerceValue(ExpandedWidthProperty);
		}

		void IMinimizeHelped.OnIsMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CoerceValue(ActiveHeaderProperty);

			if (part_contentHost != null)
				widthDelta = ActualWidth - part_contentHost.ActualWidth;

			RaiseVisibleItemsChangedEvent();
			RaiseMinimizedChangedEvent();
		}
		
		public override void BeginInit()
		{
			base.BeginInit();
			minimizeHelper = new MinimizeHelper(this);
		}

		void ConfigureMenu_Closed(object sender, RoutedEventArgs e)
		{
			IsConfigureMenuOpen = false;
		}

		#region CONTEXT MENUS

		internal static void SetConfigmenuPlacementTarget(NavigationPane d, ContextMenu menu, UIElement target)
		{
			if (target != null)
			{
				menu.Placement = PlacementMode.Right;
				menu.PlacementTarget = target;
			}
			else
			{
				menu.Placement = PlacementMode.MousePoint;
				menu.PlacementTarget = d;
			}
		}

		public event RoutedEventHandler ConfigureWindowOpened
		{
			add { AddHandler(ConfigureWindowOpenedEvent, value); }
			remove { RemoveHandler(ConfigureWindowOpenedEvent, value); }
		}
		public static readonly RoutedEvent ConfigureWindowOpenedEvent = EventManager.RegisterRoutedEvent(
						"ConfigureWindowOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPane));

		private bool RaiseConfigureWindowOpenedEvent()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(ConfigureWindowOpenedEvent);
			RaiseEvent(newEventArgs);

			return !newEventArgs.Handled;
		}

		void ConfigureButtonClick(object sender, RoutedEventArgs e)
		{
			if (ConfigureMenu != null)
				IsConfigureMenuOpen = true;
		}

		public ContextMenu ItemsButtonsContextMenu
		{
			get { return (ContextMenu)GetValue(ItemsButtonsContextMenuProperty); }
			set { SetValue(ItemsButtonsContextMenuProperty, value); }
		}
		public static readonly DependencyProperty ItemsButtonsContextMenuProperty =
						DependencyProperty.Register("ItemsButtonsContextMenu", typeof(ContextMenu), typeof(NavigationPane), null);

		public ContextMenu ConfigureMenu
		{
			get { return (ContextMenu)GetValue(ConfigureMenuProperty); }
			set { SetValue(ConfigureMenuProperty, value); }
		}
		public static readonly DependencyProperty ConfigureMenuProperty =
						DependencyProperty.Register("ConfigureMenu", typeof(ContextMenu), typeof(NavigationPane), null);

		public bool IsDefaultConfigureMenuEnabled
		{
			get { return (bool)GetValue(IsDefaultConfigureMenuEnabledProperty); }
			set { SetValue(IsDefaultConfigureMenuEnabledProperty, value); }
		}
		// Using a DependencyProperty as the backing store for IsDefaultConfigureMenuEnabled.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsDefaultConfigureMenuEnabledProperty =
						DependencyProperty.Register("IsDefaultConfigureMenuEnabled", typeof(bool), typeof(NavigationPane),
						new UIPropertyMetadata(true));

		public bool IsConfigureMenuOpen
		{
			get { return (bool)GetValue(IsConfigureMenuOpenProperty); }
			set { SetValue(IsConfigureMenuOpenProperty, value); }
		}
		public static readonly DependencyProperty IsConfigureMenuOpenProperty =
						DependencyProperty.Register("IsConfigureMenuOpen", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(false, new PropertyChangedCallback(OnIsConfigureMenuOpenChenged)));

		private static void OnIsConfigureMenuOpenChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NavigationPane p = d as NavigationPane;
			p.ConfigureMenu.IsOpen = (bool)e.NewValue;
		}

		#endregion configuremenu

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			ItemContainerGenerator.StatusChanged += new EventHandler(ItemContainerGenerator_StatusChanged);
		}

		void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
		{
			if (base.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
			{
				if (base.HasItems && (base.SelectedIndex < 0))
					base.SelectedIndex = 0;

				this.UpdateSelectedContent();
			}
		}

		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			if (((base.SelectedIndex == -1) && (base.Items.Count > 0)) && (base.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated))
			{
				foreach (object obj2 in (IEnumerable)base.Items)
				{
					if (obj2 is UIElement)
					{
						UIElement element = obj2 as UIElement;
						if (element.Visibility != Visibility.Visible)
							continue;
						base.SelectedItem = obj2;
					}
					else
					{
						base.SelectedItem = obj2;
					}
					break;
				}
			}
			UpdateSelectedContent();
			base.OnItemsChanged(e);
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			base.OnSelectionChanged(e);
			UpdateSelectedContent();
			CommandManager.InvalidateRequerySuggested();
			if (Command != null)
			{
				RoutedCommand command = Command as RoutedCommand;

				if (command != null)
					command.Execute(CommandParameter, CommandTarget);
				else
					((ICommand)Command).Execute(CommandParameter);
			}

		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NavigationPaneItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is NavigationPaneItem;
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);

			FrameworkElement fel = element as FrameworkElement;
			if(fel != null && fel.ContextMenu == null)
				fel.ContextMenu = ItemsButtonsContextMenu;
		}


  protected override void ClearContainerForItemOverride(DependencyObject element, object item)
  {
   base.ClearContainerForItemOverride(element, item);
   FrameworkElement fel = element as FrameworkElement;
   if (fel != null && fel.ContextMenu != null)
    fel.ContextMenu = null;
  }

		private void UpdateSelectedContent()
		{
			CoerceValue(SelectedItemSubItemsProperty);
			if (SelectedIndex < 0)
			{
				SelectedContent = null;
				SelectedHeader = null;
			}
			else
			{
				HeaderedContentControl control = this.CurrentItemsAsHeaderedControl();
				if (control != null)
				{
					//SelectedItem = control as NavigationPaneItem;
					SelectedContent = control.Content;

					SelectedHeader = XamlHelper.CloneUsingXaml(control.Header);
					if ((control.ContentTemplate != null) || (control.ContentTemplateSelector != null))
					{
						SelectedContentTemplate = control.ContentTemplate;
						SelectedContentTemplateSelector = control.ContentTemplateSelector;
					}
					else
					{
						SelectedContentTemplate = ContentTemplate;
						SelectedContentTemplateSelector = ContentTemplateSelector;
					}
				}
			}
			CoerceValue(ActiveHeaderProperty);
		}

		private HeaderedContentControl CurrentItemsAsHeaderedControl()
		{
			HeaderedContentControl selectedItem = base.SelectedItem as HeaderedContentControl;
			if ((selectedItem == null) && (base.SelectedItem != null))
			{
				selectedItem = base.ItemContainerGenerator.ContainerFromItem(base.SelectedItem) as HeaderedContentControl;
			}
			return selectedItem;
		}

		public object ConfigureButtonToolTip
		{
			get { return (object)GetValue(ConfigureButtonToolTipProperty); }
			set { SetValue(ConfigureButtonToolTipProperty, value); }
		}
		public static readonly DependencyProperty ConfigureButtonToolTipProperty =
						DependencyProperty.Register("ConfigureButtonToolTip", typeof(object), typeof(NavigationPane), new UIPropertyMetadata(NPR.NavigationPane_ConfigureButtonToolTip));


		public object ActiveHeader
		{
			get { return (object)GetValue(ActiveHeaderProperty); }
			protected set { SetValue(ActiveHeaderPropertyKey, value); }
		}
		private static readonly DependencyPropertyKey ActiveHeaderPropertyKey =
						DependencyProperty.RegisterReadOnly("ActiveHeader", typeof(object), typeof(NavigationPane), new UIPropertyMetadata(null, null, CoerceActiveHeader));
		public static readonly DependencyProperty ActiveHeaderProperty = ActiveHeaderPropertyKey.DependencyProperty;

		private static object CoerceActiveHeader(DependencyObject d, object value)
		{
			NavigationPane p = d as NavigationPane;
			if (p.IsMinimized)
				return p.BarTitle;
			return p.SelectedHeader;
		}

		[ 
			Description("Indicates if a key binding should be created for the first 9 items") ,
			Category("ItemsKeyBinding")
		]
		public bool ItemsKeyAuto
		{
			get { return (bool)GetValue(AutoItemKeyBindingsProperty); }
			set { SetValue(AutoItemKeyBindingsProperty, value); }
		}
		public static readonly DependencyProperty AutoItemKeyBindingsProperty =
						DependencyProperty.Register("AutoItemKeyBindings", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(true));

		[
			Description("The combination of modifiers to use for items key binding, to allow avoid conflicts with app key if present"),
			Category("ItemsKeyBinding")
		]
		public ModifierKeys ItemsKeyModifiers
		{
			get { return (ModifierKeys)GetValue(ItemsModifierKeysProperty); }
			set { SetValue(ItemsModifierKeysProperty, value); }
		}
		public static readonly DependencyProperty ItemsModifierKeysProperty =
						DependencyProperty.Register("ItemsModifierKeys", typeof(ModifierKeys), typeof(NavigationPane), new UIPropertyMetadata(ModifierKeys.Control));


		#region StateManagemet

		public override void EndInit()
		{
   // to fix - when in interoperability mode Application.Current doesn't exists
   // so in interoperaboility auto save will not work
   if (Application.Current != null)
    Application.Current.Exit += new ExitEventHandler(OnAppExit);

   // loads a default theme if any has been merged with xaml
   if(ThemeManager.GetActiveTheme() == null)
    ThemeManager.SetActiveTheme(NavigationPaneTheme.Office2010Silver);

			if (AutoStateManagement)
				LoadState();
			base.EndInit();
		}

		void OnAppExit(object sender, ExitEventArgs e)
		{
			if(AutoStateManagement)
				NavigationPaneStateHelper.OnAppExit(this);
		}

		public bool AutoStateManagement
		{
			get { return (bool)GetValue(AutoStateManagementProperty); }
			set { SetValue(AutoStateManagementProperty, value); }
		}
		public static readonly DependencyProperty AutoStateManagementProperty =
						DependencyProperty.Register("AutoStateManagement", typeof(bool), typeof(NavigationPane), new UIPropertyMetadata(false));
		
		/// <summary>
		/// Loads the control state from a stream
		/// </summary>
		/// <returns>true if succedded</returns>
		public void LoadState(Stream stream)
		{
			NavigationPaneStateHelper.LoadState(this, stream);
		}
		/// <summary>
		/// Loads the control state from a specified file
		/// </summary>
		/// <returns>true if succedded</returns>
		public void LoadState(string fileName)
		{
			NavigationPaneStateHelper.LoadState(this, fileName);
		}
		/// <summary>
		/// Loads the control state from an IsolatedStorage ( default when AutoStateManagement = true )
		/// </summary>
		/// <returns>true if succedded</returns>
		public void LoadState()
		{
			NavigationPaneStateHelper.LoadState(this);
		}

		/// <summary>
		/// Saves the control state from a specified file
		/// </summary>
		/// <returns>true if succedded</returns>
		public void SaveState(Stream stream)
		{
			NavigationPaneStateHelper.SaveState(this, stream);
		}
		/// <summary>
		/// Saves the control state from a specified file
		/// </summary>
		/// <returns>true if succedded</returns>
		public void SaveState(string fileName)
		{
			NavigationPaneStateHelper.SaveState(this, fileName);
		}
		/// <summary>
		/// Saves the control state from a specified file
		/// </summary>
		/// <returns>true if succedded</returns>
		public void SaveState()
		{
			NavigationPaneStateHelper.SaveState(this);
		}


		public void ResetSavedState(Stream stream)
		{
			NavigationPaneStateHelper.ResetState(this, stream);
		}
		public void ResetSavedState(string fileName)
		{
			NavigationPaneStateHelper.ResetState(this, fileName);
		}
		public void ResetSavedState()
		{
			NavigationPaneStateHelper.ResetState(this);
		}
		#endregion


		// Make Command a dependency property so it can use databinding.
		public static readonly DependencyProperty CommandProperty =
						DependencyProperty.Register("Command", typeof(ICommand), typeof(NavigationPane), new PropertyMetadata((ICommand)null, new PropertyChangedCallback(CommandChanged)));

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
			NavigationPane item = (NavigationPane)d;
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

			if (Command != null)
			{
				RoutedCommand command = Command as RoutedCommand;

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
						DependencyProperty.Register("CommandParameter", typeof(object), typeof(NavigationPane), new UIPropertyMetadata(null));

		public IInputElement CommandTarget
		{
			get { return (IInputElement)GetValue(CommandTargetProperty); }
			set { SetValue(CommandTargetProperty, value); }
		}
		// Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandTargetProperty =
						DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(NavigationPane), new UIPropertyMetadata(null));
	}
}
