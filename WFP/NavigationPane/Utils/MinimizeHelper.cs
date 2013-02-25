using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using NavigationPane.Properties;

namespace Stema.Controls.Utils
{
 internal interface IMinimizeHelped
 {
  MinimizeHelper ResizeHelper { get; }
  void OnIsMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
 }

 internal sealed class MinimizeHelper
 {
  FrameworkElement _owner;
  Thumb _thumb;
  FrameworkElement _sizeProvider;
  FrameworkElement _contentHost;

  double _minWidth;
  double _expandedWidth;
  internal double ExpandedWidth { get { return _expandedWidth; } set { _expandedWidth = value; } }
  double _expandedDelta;
  internal double ExpandedDelta { get { return _expandedDelta; } set { _expandedDelta = value; } }
  
  internal bool isResizing;
  internal bool IsResizing { get { return isResizing; } }

  private bool IsNavigationPane { get { return _owner is NavigationPane; } }

  internal MinimizeHelper(FrameworkElement owner)
  {
   if (!(owner is IMinimizeHelped))
    throw new ArgumentException(NPR.Error_MinimizeHelperOwner, "owner");

   _owner = owner;
   (_owner as FrameworkElement).Loaded += new RoutedEventHandler(NavigationPaneExpander_Loaded);
  }

  private void NavigationPaneExpander_Loaded(object sender, RoutedEventArgs e)
  {
   if (IsNavigationPane)
    (sender as DependencyObject).CoerceValue(NavigationPane.IsMinimizedProperty);
   else
    (sender as DependencyObject).CoerceValue(NavigationPaneExpander.IsMinimizedProperty);
  }

  internal void Init(Thumb thumb, FrameworkElement sizeProvider, FrameworkElement contentHost)
  {
   if (thumb == null || sizeProvider == null || contentHost == null)
    return;
    //throw new NullReferenceException(); 
   // causes some problems with the designer
   // commented till the designers dlls are done... they would solve this problem

   _thumb = thumb;
   if (_thumb != null)
   {
    _thumb.DragStarted += new DragStartedEventHandler(part_thumb_DragStarted);
    _thumb.DragDelta += new DragDeltaEventHandler(part_thumb_DragDelta);
    _thumb.DragCompleted += new DragCompletedEventHandler(part_thumb_DragCompleted);
   }

   _contentHost = contentHost;
   _sizeProvider = sizeProvider;
   if (_sizeProvider != null)
   {
    Binding b = new Binding();
    b.Source = _sizeProvider;
    b.Path = new PropertyPath(FrameworkElement.ActualWidthProperty);
    if(IsNavigationPane)
     (_owner as FrameworkElement).SetBinding(NavigationPane.MinimizedMinWidthProperty, b);
    else
     (_owner as FrameworkElement).SetBinding(NavigationPaneExpander.MinimizedMinWidthProperty, b);
   }
  }

  private double Width
  {
   get { return (double)_owner.GetValue(FrameworkElement.WidthProperty); }
   set { _owner.SetValue(FrameworkElement.WidthProperty, value); }
  }
  private double MinWidth 
  {
   get { return (double)_owner.GetValue(FrameworkElement.MinWidthProperty); }
   set { _owner.SetValue(FrameworkElement.MinWidthProperty, value); }
  }
  private double MaxWidth { get { return (double)_owner.GetValue(FrameworkElement.MaxWidthProperty); } }
  private double ActualWidth { get { return (double)_owner.GetValue(FrameworkElement.ActualWidthProperty); } }
  private double MinimizedMinWidth 
  { 
   get 
   { 
    if(IsNavigationPane)
     return (double)_owner.GetValue(NavigationPane.MinimizedMinWidthProperty);
    else
     return (double)_owner.GetValue(NavigationPaneExpander.MinimizedMinWidthProperty); 
   } 
  }

  private bool CanMinimize
  {
   get
   {
    if (IsNavigationPane)
     return (bool)_owner.GetValue(NavigationPane.CanMinimizeProperty);
    else
     return (bool)_owner.GetValue(NavigationPaneExpander.CanMinimizeProperty);
   }
   set
   {
    if (IsNavigationPane)
     _owner.SetValue(NavigationPane.CanMinimizeProperty, value);
    else
     _owner.SetValue(NavigationPaneExpander.CanMinimizeProperty, value);
   }
  }

  internal bool IsMinimized 
  { 
   get 
   { 
    if(IsNavigationPane)
     return (bool)_owner.GetValue(NavigationPane.IsMinimizedProperty); 
    else
     return (bool)_owner.GetValue(NavigationPaneExpander.IsMinimizedProperty); 
   }
   set 
   {
    if (IsNavigationPane)
     _owner.SetValue(NavigationPane.IsMinimizedProperty, value); 
    else
     _owner.SetValue(NavigationPaneExpander.IsMinimizedProperty, value);
   }
  }

  void part_thumb_DragCompleted(object sender, DragCompletedEventArgs e)
  {
   isResizing = false;
  }

  private void UpdateLayout()
  {
   (_owner as UIElement).UpdateLayout();
  }

  void part_thumb_DragDelta(object sender, DragDeltaEventArgs e)
  {
   double width = ActualWidth;
   if (DockPanel.GetDock(sender as UIElement)== Dock.Right)
    width += e.HorizontalChange;
   else
    width += -e.HorizontalChange;

   if (CanMinimize)
   {
    if (IsMinimized && width > MinimizedMinWidth)
    {
     IsMinimized = false;
     UpdateLayout();
    }
    else if (!IsMinimized && width < MinimizedMinWidth)
    {
     width = MinimizedMinWidth;
     IsMinimized = true;
     UpdateLayout();
    }
   }

   if (width < MaxWidth && width > MinWidth)
    Width = width;
  }

  void part_thumb_DragStarted(object sender, DragStartedEventArgs e)
  {
   _expandedWidth = ActualWidth;
   isResizing = true;
  }

  //since the size depends on _contentHost kwhen the statemanager reset the control state
  //should call this funzction to allow correctly resize _controlHost too 
  internal void ResetMinizedSize()
  {
   if (IsMinimized)
   {
    _contentHost.Width = _expandedWidth - _expandedDelta;
    Width = double.NaN;
    _minWidth = MinWidth;
    MinWidth = MinimizedMinWidth;
   }
  }

  // I say it in italan ( as I'don't know how can I say it in english) :
  // un po' contorto, ma pare che 'funzionicchiucciolina' !!! )P
  internal void AdjustSizes(bool isMinimized)
  {
   if (_contentHost == null)
    return;

   NavigationPaneExpander source = _owner as NavigationPaneExpander;
   if (source != null && source.IsInsideNavigationPane)
    return;

   if (isMinimized)
   {
    if (!isResizing)
     _expandedWidth = Width;

    _contentHost.Width = isResizing ? _expandedWidth : _contentHost.ActualWidth;
    _expandedDelta = _expandedWidth - _contentHost.Width;
    Width = double.NaN;

    _minWidth = MinWidth;
    MinWidth = MinimizedMinWidth;
   }
   else
   {
    if (!isResizing)
    {
     Width = _contentHost.Width + _expandedDelta;
    }

    _contentHost.Width = double.NaN;
    MinWidth = _minWidth;
   }
  }

  internal static object CoerceIsMinimized(DependencyObject d, object value)
  {
   FrameworkElement element = d as FrameworkElement;
   if (!element.IsInitialized)
    return false;
   return value;
  }

  internal static void OnIsMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   if(d is NavigationPane)
    d.CoerceValue(NavigationPane.ButtonToolTipProperty);
   else
    d.CoerceValue(NavigationPaneExpander.ButtonToolTipProperty);

   IMinimizeHelped helped = d as IMinimizeHelped;
   if (helped != null)
   {
    helped.OnIsMinimizedChanged(d, e);
    helped.ResizeHelper.AdjustSizes((bool)e.NewValue); 
   }
  }

  internal static void ButtonToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   if(d is NavigationPane)
    d.CoerceValue(NavigationPane.ButtonToolTipProperty);
   else
    d.CoerceValue(NavigationPaneExpander.ButtonToolTipProperty);
  }

  internal static object CoerceButtonToolTip(DependencyObject d, object value)
  {
   if(d is NavigationPane)
    return (bool)d.GetValue(NavigationPane.IsMinimizedProperty) ? 
     d.GetValue(NavigationPane.ExpandToolTipProperty) : d.GetValue(NavigationPane.MinimizeToolTipProperty);
   else
    return (bool)d.GetValue(NavigationPaneExpander.IsMinimizedProperty) ?
     d.GetValue(NavigationPaneExpander.ExpandToolTipProperty) : d.GetValue(NavigationPaneExpander.MinimizeToolTipProperty);
  }

  internal static object CoerceMinimizedMinWidth(DependencyObject d, object value)
  {
   IMinimizeHelped helped = d as IMinimizeHelped;
   if (helped != null)
   {
    Control ctrl = d as Control;
    double result = (double)value + ctrl.Margin.Left + ctrl.Margin.Right;
    if (helped.ResizeHelper._thumb != null)
     result += helped.ResizeHelper._thumb.ActualWidth;
    return result;
   }
   return value;
  }
 }
}
