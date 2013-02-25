#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using NavigationPane.Properties;
using System.Windows.Interop;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Stema.Controls.Utils;
using System.Linq;
using System.IO;

namespace Stema.Controls
{
	/// <summary>

	/// </summary>
	[
		TemplatePart(Name = NavigationPaneExpanderParts.PART_ContentHost, Type = typeof(ItemsPresenter)),
		TemplatePart(Name = NavigationPaneExpanderParts.PART_ContentHostContainerUnexpanded, Type = typeof(Border)),
		TemplatePart(Name = NavigationPaneExpanderParts.PART_ContentHostContainerExpanded, Type = typeof(Border)),

		TemplatePart(Name = NavigationPaneExpanderParts.PART_HeaderHost, Type = typeof(Border)),
		TemplatePart(Name = NavigationPaneExpanderParts.PART_HeaderHostMinimized, Type = typeof(Border)),

		TemplatePart(Name = NavigationPaneExpanderParts.PART_Popup, Type = typeof(Popup)),
		TemplatePart(Name = NavigationPaneExpanderParts.PART_CloseButton, Type = typeof(ToggleButton)),

		TemplatePart(Name = NavigationPaneExpanderParts.PART_ResizeThumb, Type = typeof(Thumb)),
		TemplatePart(Name = NavigationPaneExpanderParts.PART_MininizedSizeProvider, Type = typeof(FrameworkElement)),

		StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationPaneItem))
	]
	public class NavigationPaneExpander : HeaderedItemsControl, IMinimizeHelped
	{
		internal static class NavigationPaneExpanderParts
		{
			public const string PART_ContentHost = "PART_ContentHost";
			public const string PART_ContentHostContainerUnexpanded = "PART_ContentHostContainerUnexpanded";
			public const string PART_ContentHostContainerExpanded = "PART_ContentHostContainerExpanded";

			public const string PART_HeaderHost = "PART_HeaderHost";
			public const string PART_HeaderHostMinimized = "PART_HeaderHostMinimized";

			public const string PART_Popup = "PART_Popup";
			public const string PART_CloseButton = "PART_CloseButton";

			public const string PART_ResizeThumb = "PART_ResizeThumb";
			public const string PART_MininizedSizeProvider = "PART_MininizedSizeProvider";
		}

		static NavigationPaneExpander()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPaneExpander), new FrameworkPropertyMetadata(typeof(NavigationPaneExpander)));
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new NavigationPaneItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is NavigationPaneItem;
		}

		#region TEMPLATED PARTS

		internal ItemsPresenter part_contentHost;
		private Border part_contentHostContainerExpanded;
		private Border part_contentHostContainerUnexpanded;

		private Border part_headerHost;
		private Border part_headerHostMinimized;

		private Popup part_popup;
		private ToggleButton part_closeButton;

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
						"MinimizedChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPaneExpander));

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
						DependencyProperty.RegisterReadOnly("ExpandedWidth", typeof(double), typeof(NavigationPaneExpander), new UIPropertyMetadata(0.0, null, CoerceExpandedWidth));
		public static readonly DependencyProperty ExpandedWidthProperty = ExpandedWidthPropertyKey.DependencyProperty;
		
		private static object CoerceExpandedWidth(DependencyObject d, object value)
		{
			NavigationPaneExpander expander = d as NavigationPaneExpander;
			if (expander.IsMinimized)
				return expander.widthDelta + expander.part_contentHost.ActualWidth;
			else
				return expander.ActualWidth;
		}

		private void part_contentHostSizechanged(object Sender, SizeChangedEventArgs e)
		{
			if(IsMinimized)
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
			CoerceValue(IsPopupOpenProperty);

			SwitchHeader((bool)e.NewValue);
			SwitchContentToPopUp((bool)e.NewValue);
			
			widthDelta = ActualWidth - part_contentHost.ActualWidth;

			RaiseMinimizedChangedEvent();
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (part_contentHost != null)
				part_contentHost.SizeChanged += part_contentHostSizechanged;
			part_contentHost = GetTemplateChild(NavigationPaneExpanderParts.PART_ContentHost) as ItemsPresenter;
			if(part_contentHost != null)
				part_contentHost.SizeChanged += part_contentHostSizechanged;

			part_contentHostContainerExpanded = GetTemplateChild(NavigationPaneExpanderParts.PART_ContentHostContainerExpanded) as Border;
			part_contentHostContainerUnexpanded = GetTemplateChild(NavigationPaneExpanderParts.PART_ContentHostContainerUnexpanded) as Border;

			part_headerHost = GetTemplateChild(NavigationPaneExpanderParts.PART_HeaderHost) as Border;
			part_headerHostMinimized = GetTemplateChild(NavigationPaneExpanderParts.PART_HeaderHostMinimized) as Border;

			part_closeButton = GetTemplateChild(NavigationPaneExpanderParts.PART_CloseButton) as ToggleButton;
			if(part_closeButton != null)
				part_closeButton.Click += new RoutedEventHandler(part_closeButton_Click);

			InitPopup();

			// check minimizeHelper because on blend when you try to edit the style BeginInit() is not already called
			// or is not called at all !! ?? Anyways... check to avoid use of null value reference
			if (minimizeHelper != null)
				minimizeHelper.Init(GetTemplateChild(NavigationPaneExpanderParts.PART_ResizeThumb) as Thumb,
					GetTemplateChild(NavigationPaneExpanderParts.PART_MininizedSizeProvider) as FrameworkElement, part_contentHost);
		}

		internal FrameworkElement Get_PART_ContentHost()
		{
			return part_contentHost;
		}

		public override void BeginInit()
		{
			base.BeginInit();
			minimizeHelper = new MinimizeHelper(this);
		}


		#endregion

		#region StateManagemet

		public override void EndInit()
		{
   // to fix - when in interoperability mode Application.Current doesn't exists
   // so in interoperaboility auto save will not work
   if (Application.Current != null)
    Application.Current.Exit += new ExitEventHandler(OnAppExit);
			if (AutoStateManagement)
				LoadState();

			base.EndInit();
		}

  void OnAppExit(object sender, ExitEventArgs e)
  {
   if (AutoStateManagement)
    NavigationPaneStateHelper.OnAppExit(this);
  }

		public bool AutoStateManagement
		{
			get { return (bool)GetValue(AutoStateManagementProperty); }
			set { SetValue(AutoStateManagementProperty, value); }
		}
		public static readonly DependencyProperty AutoStateManagementProperty =
						DependencyProperty.Register("AutoStateManagement", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(false));
		
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

		#region  EXPAND HANDLING

		public object ExpandToolTip
		{
			get { return (object)GetValue(ExpandToolTipProperty); }
			set { SetValue(ExpandToolTipProperty, value); }
		}
		public static readonly DependencyProperty ExpandToolTipProperty =
						DependencyProperty.Register("ExpandToolTip", typeof(object), typeof(NavigationPaneExpander),
						new UIPropertyMetadata(NPR.NavigationPaneExpander_ExpandToolTip, MinimizeHelper.ButtonToolTipChanged));

		public object MinimizeToolTip
		{
			get { return (object)GetValue(MinimizeToolTipProperty); }
			set { SetValue(MinimizeToolTipProperty, value); }
		}
		public static readonly DependencyProperty MinimizeToolTipProperty =
						DependencyProperty.Register("MinimizeToolTip", typeof(object), typeof(NavigationPaneExpander),
						new UIPropertyMetadata(NPR.NavigationPaneExpander_MinimizeToolTip, MinimizeHelper.ButtonToolTipChanged));

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ButtonToolTip
		{
			get { return (object)GetValue(ButtonToolTipProperty); }
		}
		internal static readonly DependencyPropertyKey ButtonToolTipPropertyKey =
						DependencyProperty.RegisterReadOnly("ButtonToolTip", typeof(object), typeof(NavigationPaneExpander),
						new UIPropertyMetadata(NPR.NavigationPaneExpander_MinimizeToolTip, null, MinimizeHelper.CoerceButtonToolTip));
		public static readonly DependencyProperty ButtonToolTipProperty = ButtonToolTipPropertyKey.DependencyProperty;

  public bool IsMinimizeButtonVisible
  {
   get { return (bool)GetValue(IsMinimizeButtonVisibleProperty); }
   set { SetValue(IsMinimizeButtonVisibleProperty, value); }
  }
  public static readonly DependencyProperty IsMinimizeButtonVisibleProperty =
      DependencyProperty.Register("IsMinimizeButtonVisible", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(true));

  public bool CanMinimize
  {
   get { return (bool)GetValue(CanMinimizeProperty); }
   set { SetValue(CanMinimizeProperty, value); }
  }
  public static readonly DependencyProperty CanMinimizeProperty =
      DependencyProperty.Register("CanMinimize", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(true));

		public bool IsMinimized
		{
			get { return (bool)GetValue(IsMinimizedProperty); }
			set { SetValue(IsMinimizedProperty, value); }
		}
		public static readonly DependencyProperty IsMinimizedProperty =
						DependencyProperty.Register("IsMinimized", typeof(bool), typeof(NavigationPaneExpander),
						new FrameworkPropertyMetadata(false,
							FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
							new PropertyChangedCallback(MinimizeHelper.OnIsMinimizedChanged), new CoerceValueCallback(MinimizeHelper.CoerceIsMinimized)));

		private delegate void SetMinimized(NavigationPaneExpander expander, bool minimized);

		private void SwitchContentToPopUp(bool minimized)
		{
			//Application.Current.Dispatcher.BeginInvoke((SetMinimized)((e, m) =>
			//{
			NavigationPaneExpander e = this;
			bool m = minimized;

			Border expanded = e.part_contentHostContainerExpanded;
			Border unexpanded = e.part_contentHostContainerUnexpanded;
			ItemsPresenter presenter = e.part_contentHost;
			if (expanded != null && unexpanded != null && presenter != null)
			{
				if (m)
				{
					expanded.Child = null;
					unexpanded.Child = presenter;
					//unexpanded.InvalidateVisual();
				}
				else
				{
					unexpanded.Child = null;
					expanded.Child = presenter;
				}
			}
			//}), DispatcherPriority.Render, this, minimized);
		}

		private void SwitchHeader(bool isMinimized)
		{
			if (part_headerHost != null && part_headerHostMinimized != null)
			{
				if (isMinimized)
				{
					UIElement o = part_headerHost.Child;
					part_headerHost.Child = null;
					part_headerHostMinimized.Child = o;
				}
				else
				{
					UIElement o = part_headerHostMinimized.Child;
					part_headerHostMinimized.Child = null;
					part_headerHost.Child = o;
				}
			}
		}

		private double MinimizedMinWidth
		{
			get { return (double)GetValue(MinimizedMinWidthProperty); }
			set { SetValue(MinimizedMinWidthProperty, value); }
		}
		internal static readonly DependencyProperty MinimizedMinWidthProperty =
						DependencyProperty.Register("MinimizedMinWidth", typeof(double), typeof(NavigationPaneExpander),
						new PropertyMetadata(0.0, null, new CoerceValueCallback(MinimizeHelper.CoerceMinimizedMinWidth)));

		public bool HasResizeThumb
		{
			get { return (bool)GetValue(HasResizeThumbProperty); }
			set { SetValue(HasResizeThumbProperty, value); }
		}
		public static readonly DependencyProperty HasResizeThumbProperty =
						DependencyProperty.Register("HasResizeThumb", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(true));

		#endregion

		#region POPUP HANDLING

		private PopupResizer popupResizer;
		private class PopupResizer : IDisposable
		{
			internal Cursor Cursor;
			internal Popup Popup;
			internal FrameworkElement ControlledElement;
			internal bool IsResizing;
			internal PopupAdorner Adorner;
			internal PopupAdorner ChildAdorner;
			internal Style PopupResizePreviewStyle;

			private Visual topMost;

			internal PopupResizer(Popup popup, FrameworkElement controlledElement, Style previewStyle)
			{
				this.ControlledElement = controlledElement;
				this.Popup = popup;
				this.PopupResizePreviewStyle = previewStyle;
				IsResizing = false;
				Adorner = null;
				this.Cursor = (popup.Child as FrameworkElement).Cursor;

				PresentationSource source = HwndSource.FromVisual(popup);
				topMost = source.RootVisual;
				SetMouseEvents();
			}

			~PopupResizer()
			{
				SetMouseEvents(true);
			}

			public void Dispose()
			{
				SetMouseEvents(true);
				GC.SuppressFinalize(this);
			}

			private void SetMouseEvents(bool remove = false)
			{
				UIElement el = topMost as UIElement;
				if (el != null)
				{
					if (remove)
					{
						el.PreviewMouseDown -= OnPreviewMouseButtonDown;
						el.MouseDown -= OnMouseButtonDown;
						el.MouseUp -= OnMouseButtonUp;
						el.MouseMove -= OnMouseMove;
					}
					else
					{
						el.PreviewMouseDown += OnPreviewMouseButtonDown;
						el.MouseDown += OnMouseButtonDown;
						el.MouseUp += OnMouseButtonUp;
						el.MouseMove += OnMouseMove;
					}
				}
			}

			internal void EndResize()
			{
				if (Adorner != null)
					(VisualTreeHelper.GetParent(Adorner) as AdornerLayer).Remove(Adorner);
				if (ChildAdorner != null)
					(VisualTreeHelper.GetParent(ChildAdorner) as AdornerLayer).Remove(ChildAdorner);

				ControlledElement.Width = Math.Max(0, ControlledElement.ActualWidth + ChildAdorner.EffectiveOffset);

				FrameworkElement element = Popup.Child as FrameworkElement;
				element.Cursor = Cursor;
				FrameworkElement e = topMost as FrameworkElement;
				if (e != null)
					e.Cursor = null;

				IsResizing = false;
			}

			internal void StartResize(Window sender)
			{
				// damn!!!! 
				// I'm forced to use two separated adorners
				// to have it displayed over the popup child
				// and to have it also outside of the child contorl
				// if you know a better way to do this
				// please post a discussion on navigationpane.codeplex.com

				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(Popup);
				if (adornerLayer != null)
				{
					Adorner = new PopupAdorner(Popup, Popup.Placement, PopupResizePreviewStyle);
					adornerLayer.Add(Adorner);
				}

				adornerLayer = AdornerLayer.GetAdornerLayer(Popup.Child);
				if (adornerLayer != null)
				{
					ChildAdorner = new PopupAdorner(Popup.Child, Popup.Placement, PopupResizePreviewStyle);
					adornerLayer.Add(ChildAdorner);
				}

				FrameworkElement child = Popup.Child as FrameworkElement;

				FrameworkElement e = topMost as FrameworkElement;
				if(e != null)
					e.Cursor = System.Windows.Input.Cursors.SizeWE;
				child.Cursor = System.Windows.Input.Cursors.SizeWE;

				IsResizing = true;
			}

			private bool CheckResizeZone()
			{
				if (Popup.Child != null)
				{
					FrameworkElement element = Popup.Child as FrameworkElement;
					Point p = Mouse.GetPosition(element);
					if (element != null)
					{
						double delta = 5;
						double x = Popup.Placement == PlacementMode.Left ? (p.X - element.ActualWidth) + delta : p.X;
						return x > -1 && x < delta;
					}
				}
				return false;
			}

			public void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (IsResizing)
				{
					FrameworkElement child = Popup.Child as FrameworkElement;
					Point p = e.MouseDevice.GetPosition(child);
					double offset = p.X;
					if (Popup.Placement == PlacementMode.Left)
						offset -= child.ActualWidth;

					Adorner.Offset = offset;
					ChildAdorner.Offset = offset;
				}
				else
				{
					FrameworkElement element = Popup.Child as FrameworkElement;
					if (element != null)
					{
						if (CheckResizeZone())
						{
							if (element.Cursor != System.Windows.Input.Cursors.SizeWE)
								element.Cursor = System.Windows.Input.Cursors.SizeWE;
						}
						else if (element.Cursor != Cursor)
							element.Cursor = Cursor;
					}
				}
			}
			public void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
			{
				if (e.LeftButton == MouseButtonState.Released)
				{
					if (IsResizing)
					{
						EndResize();
						e.Handled = true;
					}
				}
			}
			public void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					if (ControlledElement != null && Popup.Child != null && !IsResizing)
					{
						if (CheckResizeZone())
						{
							StartResize(sender as Window);
							e.Handled = true;
						}
					}
				}
			}
			public void OnPreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
			{
				if (CheckClose(Popup, (DependencyObject)e.OriginalSource))
				{
					Popup.IsOpen = false;
					e.Handled = true;
				}
			}

		}

		#region PopupAdorner

		private class PopupAdorner : Adorner
		{
			private Decorator _decorator;
			private TranslateTransform Translation;
			private PlacementMode PlacementMode;

			internal PopupAdorner(UIElement element, PlacementMode placementMode, Style previewStyle)
				: base(element)
			{
				PlacementMode = placementMode;

				Control control = new Control();
				control.SnapsToDevicePixels = true;
				control.Style = previewStyle;
				control.IsEnabled = false;
				Translation = new TranslateTransform();
				_decorator = new Decorator();
				_decorator.Child = control;
				_decorator.RenderTransform = Translation;
				AddVisualChild(_decorator);
			}

			protected override Size MeasureOverride(Size constraint)
			{
				Size s = Size.Empty;
				FrameworkElement element = null;
				Popup p = AdornedElement as Popup;
				if (p != null)
				{
					element = p.Child as FrameworkElement;
					s = element.RenderSize;
					s.Width = 1;
					//s.Height -= element.Margin.Top + element.Margin.Bottom;
				}
				else
				{
					s = base.MeasureOverride(constraint);
					if (PlacementMode == PlacementMode.Left && Offset > 0)
						s.Width -= (AdornedElement as FrameworkElement).Margin.Right;
				}

				double offset = Offset;
				if (PlacementMode == PlacementMode.Right)
					offset *= -1;

				if (element != null && (PlacementMode == PlacementMode.Right) && Offset > element.DesiredSize.Width - (element.Margin.Left + element.Margin.Right))
					s.Width = 0;
				if (s.Width + offset > 0)
					s.Width += offset;

				return s;
			}

			protected override Size ArrangeOverride(Size finalSize)
			{
				Popup p = AdornedElement as Popup;
				if (p != null)
				{
					FrameworkElement element = p.Child as FrameworkElement;
					Point loc = element.TranslatePoint(new Point(), p);
					loc.Y += element.Margin.Top;
					if (PlacementMode == PlacementMode.Left)
						loc.X += element.ActualWidth - element.Margin.Right;
					_decorator.Arrange(new Rect(loc, finalSize));
				}
				else
					_decorator.Arrange(new Rect(new Point(), finalSize));
				return finalSize;
			}

			protected override Visual GetVisualChild(int index)
			{
				if (index != 0)
					throw new ArgumentOutOfRangeException("index");
				return this._decorator;
			}

			private double offset;
			public double Offset
			{
				get { return offset; }
				set
				{
					offset = value;
					if (PlacementMode == PlacementMode.Right)
						Translation.X = offset;
					InvalidateMeasure();
				}
			}

			public double EffectiveOffset
			{
				get
				{
					if (PlacementMode == PlacementMode.Right)
						return offset * -1;
					return offset;
				}
			}

			protected override int VisualChildrenCount
			{
				get { return 1; }
			}
		}

		#endregion

		private void InitPopup()
		{
			part_popup = GetTemplateChild(NavigationPaneExpanderParts.PART_Popup) as Popup;
			if (part_popup != null)
			{
				//HwndSource source = HwndSource.FromVisual(this) as HwndSource;
				//if (source != null)
				//{
				// source.AddHook(new HwndSourceHook(WndProc));
				//}

				// this actually works only on wpf apps ... no interoperaability no silverlight
				Window w = Window.GetWindow(this);
				if (w != null)
				{
					// title click and app deactivate close popup
					w.SourceInitialized += new EventHandler(w_SourceInitialized);
					w.Deactivated += new EventHandler(window_Deactivated);
				}

				part_popup.Closed += new EventHandler(OnPopupClosed);
			}
		}

		void w_SourceInitialized(object sender, EventArgs e)
		{
			HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(sender as Window).Handle);
			source.AddHook(new HwndSourceHook(WndProc));
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 0x00A1 /* WM_NCLBUTTONDOWN */ && IsPopupOpen)
			{
				Close();
				handled = true;
			}
			else
				handled = false;
			return IntPtr.Zero;
		}

		private void window_Deactivated(object sender, EventArgs e)
		{
			if (IsPopupOpen)
				Close();
		}

		public bool IsPopupOpen
		{
			get { return (bool)GetValue(IsPopupOpenProperty); }
			set { SetValue(IsPopupOpenProperty, value); }
		}
		public static readonly DependencyProperty IsPopupOpenProperty =
						DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(NavigationPaneExpander),
						new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsPopupOpenChanged, new CoerceValueCallback(CoerceIsPopupOpen)));

		private static object CoerceIsPopupOpen(DependencyObject d, object value)
		{
			NavigationPaneExpander p = d as NavigationPaneExpander;
			return p.IsMinimized ? value : false;
		}

		private static void IsPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NavigationPaneExpander expander = (NavigationPaneExpander)d;
			bool newValue = (bool)e.NewValue;
			bool oldValue = !newValue;

			// todo: implement automation
			//ComboBoxAutomationPeer peer = UIElementAutomationPeer.FromElement(element) as ComboBoxAutomationPeer;
			//if (peer != null)
			//{
			// peer.RaiseExpandCollapseAutomationEvent(oldValue, newValue);
			//}

			if (newValue)
			{
				if (expander.part_popup != null)
					expander.popupResizer = new PopupResizer(expander.part_popup, expander.part_contentHost, expander.PopupResizePreviewStyle);
				expander.OnPopupOpened();
			}
			else
			{
				expander.OnPopupClosed();
				if (expander.popupResizer != null)
					expander.popupResizer.Dispose();
				expander.popupResizer = null;
			}
		}

		#region EVENTS

		public event RoutedEventHandler PopupOpened
		{
			add { AddHandler(PopupOpenedEvent, value); }
			remove { RemoveHandler(PopupOpenedEvent, value); }
		}
		public static readonly RoutedEvent PopupOpenedEvent = EventManager.RegisterRoutedEvent(
						"PopupOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPaneExpander));

		protected virtual void OnPopupOpened()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(PopupOpenedEvent);
			RaiseEvent(newEventArgs);
		}

		public event RoutedEventHandler PopupClosed
		{
			add { AddHandler(PopupClosedEvent, value); }
			remove { RemoveHandler(PopupClosedEvent, value); }
		}
		public static readonly RoutedEvent PopupClosedEvent = EventManager.RegisterRoutedEvent(
						"PopupClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPaneExpander));

		protected virtual void OnPopupClosed()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(PopupClosedEvent);
			RaiseEvent(newEventArgs);
		}

		private void OnPopupClosed(object source, EventArgs e)
		{
			OnPopupClosed();
		}

		#endregion

		private bool HasCapture
		{
			get { return (Mouse.Captured == this); }
		}

		private void Close()
		{
			if (IsPopupOpen)
			{
				if (popupResizer != null && popupResizer.IsResizing)
					popupResizer.EndResize();
					#if (DOTNET_4)
						SetCurrentValue(IsPopupOpenProperty, false);
					#else
						SetValue(IsPopupOpenProperty, false);
					#endif
			}
		}

		private static bool CheckClose(Popup popup, DependencyObject element)
		{
			if (popup != null && popup.Child != null)
			{
				object o = popup.Child.FindCommonVisualAncestor(element);
				return popup.FindCommonVisualAncestor(element) != null;
			}
			return false;
		}

		public Style PopupResizePreviewStyle
		{
			get { return (Style)GetValue(PopupResizePreviewStyleProperty); }
			set { SetValue(PopupResizePreviewStyleProperty, value); }
		}
		public static readonly DependencyProperty PopupResizePreviewStyleProperty =
						DependencyProperty.Register("PopupResizePreviewStyle", typeof(Style), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));

		#endregion

		public static bool GetCanResize(DependencyObject obj)
		{
			return (bool)obj.GetValue(CanResizeProperty);
		}
		public static void SetCanResize(DependencyObject obj, bool value)
		{
			obj.SetValue(CanResizeProperty, value);
		}
		public static readonly DependencyProperty CanResizeProperty =
						DependencyProperty.RegisterAttached("CanResize", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(true));

		public Dock Orientation
		{
			get { return (Dock)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
		public static readonly DependencyProperty OrientationProperty =
						DependencyProperty.Register("Orientation", typeof(Dock), typeof(NavigationPaneExpander), new UIPropertyMetadata(Dock.Left));

		public object BarToolTip
		{
			get { return (object)GetValue(BarToolTipProperty); }
			set { SetValue(BarToolTipProperty, value); }
		}
		public static readonly DependencyProperty BarToolTipProperty =
						DependencyProperty.Register("BarToolTip", typeof(object), typeof(NavigationPaneExpander), new UIPropertyMetadata(NPR.NavigationPaneExpander_BarToolTip));

		public bool IsHeaderVisible
		{
			get { return (bool)GetValue(IsHeaderVisibleProperty); }
			set { SetValue(IsHeaderVisibleProperty, value); }
		}
		public static readonly DependencyProperty IsHeaderVisibleProperty =
						DependencyProperty.Register("IsHeaderVisible", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(false));

		public bool IsCloseButtonVisible
		{
			get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
			set { SetValue(IsCloseButtonVisibleProperty, value); }
		}
		public static readonly DependencyProperty IsCloseButtonVisibleProperty =
						DependencyProperty.Register("IsCloseButtonVisible", typeof(bool), typeof(NavigationPaneExpander), new UIPropertyMetadata(false));

		void part_closeButton_Click(object sender, RoutedEventArgs e)
		{
			((ToggleButton)sender).IsChecked = false;
			RaiseEvent(new RoutedEventArgs(CloseButtonClickEvent));
		}

		public ICommand CloseCommand
		{
			get { return (ICommand)GetValue(CloseCommandProperty); }
			set { SetValue(CloseCommandProperty, value); }
		}
		public static readonly DependencyProperty CloseCommandProperty =
						DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));

		public object CloseCommandParameter
		{
			get { return (object)GetValue(CloseCommandParameterProperty); }
			set { SetValue(CloseCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty CloseCommandParameterProperty =
						DependencyProperty.Register("CloseCommandParameter", typeof(object), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));

		public IInputElement CloseCommandTarget
		{
			get { return (IInputElement)GetValue(CloseCommandTargetProperty); }
			set { SetValue(CloseCommandTargetProperty, value); }
		}
		public static readonly DependencyProperty CloseCommandTargetProperty =
						DependencyProperty.Register("CloseCommandTarget", typeof(IInputElement), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));

		public event RoutedEventHandler CloseButtonClick
		{
			add { AddHandler(CloseButtonClickEvent, value); }
			remove { RemoveHandler(CloseButtonClickEvent, value); }
		}
		public static readonly RoutedEvent CloseButtonClickEvent = EventManager.RegisterRoutedEvent(
						"CloseButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationPaneExpander));

		/// <summary>
		/// True if we are inside a NavigationPane
		/// </summary>
		public bool IsInsideNavigationPane
		{
			get { return (bool)GetValue(IsInsideNavigationPaneProperty); }
			protected set { SetValue(IsInsideNavigationPanePropertyKey, value); }
		}
		private static readonly DependencyPropertyKey IsInsideNavigationPanePropertyKey =
						DependencyProperty.RegisterReadOnly("IsInsideNavigationPane", typeof(bool), typeof(NavigationPaneExpander),
						new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public static readonly DependencyProperty IsInsideNavigationPaneProperty = IsInsideNavigationPanePropertyKey.DependencyProperty;

		protected override void OnVisualParentChanged(DependencyObject oldParent)
		{
			base.OnVisualParentChanged(oldParent);
			IsInsideNavigationPane = TemplatedParent is NavigationPane;
		}

		protected override void ClearContainerForItemOverride(DependencyObject element, object item)
		{
			base.ClearContainerForItemOverride(element, item);
			if (item is HeaderedContentControl)
			{
				// restores the item header removed by NavigationPaneExpanderItemsHeader.FromHeaderedContentControl
				// in PrepareContainerForItemOverride
				(item as HeaderedContentControl).Header = (element as NavigationPaneItem).Header;

				IEnumerable<NavigationPaneExpanderItemsHeader> result = itemsHeaders.Where(bf => bf.Control == item);
				if (result.Count() > 0)
				{
					NavigationPaneExpanderItemsHeader data = result.First();
					itemsHeaders.Remove(data);
					data.Dispose();
				}
			}
		}

  private void CreateItemsHeaders()
  {
   if (itemsHeaders == null)
   {
    itemsHeaders = new ObservableCollection<NavigationPaneExpanderItemsHeader>();
    ItemsHeaders = new ReadOnlyObservableCollection<NavigationPaneExpanderItemsHeader>(itemsHeaders);
   } 
  }

  protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
  {
   if (itemsHeaders != null)
   {
    itemsHeaders.Clear();
    CreateItemsHeaders();
   }
   base.OnItemsSourceChanged(oldValue, newValue);
  }

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
   CreateItemsHeaders();

   if (item is HeaderedContentControl)
    itemsHeaders.Add(new NavigationPaneExpanderItemsHeader(item));

   base.PrepareContainerForItemOverride(element, item);

   if (!(item is FrameworkElement))
   {
    NavigationPaneItem i = element as NavigationPaneItem;
    if (i != null)
    {
     i.ContentTemplate = ItemTemplate;
     i.ContentTemplateSelector = ItemTemplateSelector;
     i.ContentStringFormat = ItemStringFormat;
    }

    itemsHeaders.Add(new NavigationPaneExpanderItemsHeader(item));
   }

    Binding b = new Binding();
    b.Source = item;
    if(item is FrameworkElement)
     b.Path = new PropertyPath(CanResizeProperty);
    else
     b.Path = new PropertyPath("CanResize");
    (element as FrameworkElement).SetBinding(CanResizeProperty, b);
  }

		private ObservableCollection<NavigationPaneExpanderItemsHeader> itemsHeaders;
		public ReadOnlyObservableCollection<NavigationPaneExpanderItemsHeader> ItemsHeaders
		{
			get { return (ReadOnlyObservableCollection<NavigationPaneExpanderItemsHeader>)GetValue(ItemsHeadersProperty); }
			set { SetValue(ItemsHeadersProperty, value); }
		}
		public static readonly DependencyProperty ItemsHeadersProperty =
						DependencyProperty.Register("ItemsHeaders", typeof(ReadOnlyObservableCollection<NavigationPaneExpanderItemsHeader>), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));

		public DataTemplate ItemsHeadersTemplate
		{
			get { return (DataTemplate)GetValue(ItemsHeadersTemplateProperty); }
			set { SetValue(ItemsHeadersTemplateProperty, value); }
		}
		public static readonly DependencyProperty ItemsHeadersTemplateProperty =
						DependencyProperty.Register("ItemsHeadersTemplate", typeof(DataTemplate), typeof(NavigationPaneExpander), new UIPropertyMetadata(null));
	}

	// INotifyPropertyChanged and INotifyPropertyChanging fix the problem with the dynamic modification
	// of either Header or image, which doesn't update in the bar button
	public class NavigationPaneExpanderItemsHeader: INotifyPropertyChanged, INotifyPropertyChanging, IDisposable
	{
		internal object Control { get; private set; }

		private ImageSource image;
		public ImageSource Image 
		{
			get { return image; } 
			internal set
			{
				if (PropertyChanging != null)
					PropertyChanging(this, new PropertyChangingEventArgs("Image"));
				image = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Image"));
			}
		}
		private object content;
		public object Content 
		{ 
			get { return content; }
			internal set
			{
				if (PropertyChanging != null)
					PropertyChanging(this, new PropertyChangingEventArgs("Content"));
				content = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Content"));
			}
		}

		internal NavigationPaneExpanderItemsHeader(object control)
		{
   if (control is HeaderedContentControl)
   {
    DependencyPropertyDescriptor d;
    if (control is NavigationPaneItem)
    {
     Image = (control as NavigationPaneItem).ImageSmall;
     d = DependencyPropertyDescriptor.FromProperty(NavigationPaneItem.ImageSmallProperty, typeof(NavigationPaneItem));
     d.AddValueChanged(control, ItemImageChanged);
    }

    HeaderedContentControl h = control as HeaderedContentControl;
    Content = h.Header;
    //(control as HeaderedContentControl).Header = null;

    d = DependencyPropertyDescriptor.FromProperty(HeaderedContentControl.HeaderProperty, typeof(HeaderedContentControl));
    d.AddValueChanged(control, ItemHeaderChanged);
    Control = control;
   }
   else
   {
     Content = control;
   }
		}

		~NavigationPaneExpanderItemsHeader()
		{
			ReleaseControl();
		}

		public void Dispose()
		{
			ReleaseControl();
			GC.SuppressFinalize(this); // Tell GC to not worry about this object anymore.
		}

		private void ReleaseControl()
		{
			DependencyPropertyDescriptor d;
   if (Control != null)
   {
    if (Control is NavigationPaneItem)
    {
     d = DependencyPropertyDescriptor.FromProperty(NavigationPaneItem.ImageSmallProperty, typeof(NavigationPaneItem));
     d.RemoveValueChanged(Control, ItemImageChanged);
    }
    d = DependencyPropertyDescriptor.FromProperty(HeaderedContentControl.HeaderProperty, typeof(HeaderedContentControl));
    d.RemoveValueChanged(Control, ItemHeaderChanged);
   }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;

		private enum ItemProperty
		{
			Header, Image
		}
		private void ItemHeaderChanged(object sender, EventArgs e)
		{
			ItemPropertyChanged(sender, ItemProperty.Header);
		}
		private void ItemImageChanged(object sender, EventArgs e)
		{
			ItemPropertyChanged(sender, ItemProperty.Image);
		}
		private void ItemPropertyChanged(object sender, ItemProperty property)
		{
			switch (property)
			{
				case ItemProperty.Header:
					Content = (sender as HeaderedContentControl).Header;
					break;
				case ItemProperty.Image:
					Image = (sender as NavigationPaneItem).ImageSmall;
					break;
			}
		}
	}
}
