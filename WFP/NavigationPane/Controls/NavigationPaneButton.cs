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
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using Stema.Controls.Utils;
using System.Windows.Input;
using System.ComponentModel;

namespace Stema.Controls
{
 public class NavigationPaneButton : ButtonBase
 {
  static NavigationPaneButton()
  {
   DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPaneButton), new FrameworkPropertyMetadata(typeof(NavigationPaneButton)));
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
  public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", 
   typeof(ImageSource), typeof(NavigationPaneButton), new FrameworkPropertyMetadata(null));

  /// <summary>
  /// The image tath will be used in SmallItems box. If left null the Image property will be used
  /// </summary>
  public ImageSource ImageSmall
  {
   get { return (ImageSource)GetValue(ImageSmallProperty); }
   set { SetValue(ImageSmallProperty, value); }
  }
  public static readonly DependencyProperty ImageSmallProperty = DependencyProperty.Register("ImageSmall", 
   typeof(ImageSource), typeof(NavigationPaneButton), new FrameworkPropertyMetadata(null));

  public NavigationPaneItemDisplayType ImageType
  {
   get { return (NavigationPaneItemDisplayType)GetValue(ImageTypeProperty); }
   set { SetValue(ImageTypeProperty, value); }
  }
  public static readonly DependencyProperty ImageTypeProperty =
      DependencyProperty.Register("ImageType", typeof(NavigationPaneItemDisplayType),
      typeof(NavigationPaneButton), new UIPropertyMetadata(NavigationPaneItemDisplayType.Undefined));

  #endregion

  public bool IsContentVisible
  {
   get { return (bool)GetValue(IsContentVisibleProperty); }
   set { SetValue(IsContentVisibleProperty, value); }
  }
  public static readonly DependencyProperty IsContentVisibleProperty =
      DependencyProperty.Register("IsContentVisible", typeof(bool), typeof(NavigationPaneButton), 
      new UIPropertyMetadata(true));

  #region Check State

  public bool IsChecked
  {
   get { return (bool)GetValue(IsCheckedProperty); }
   set { SetValue(IsCheckedProperty, value); }
  }
  public static readonly DependencyProperty IsCheckedProperty =
      DependencyProperty.Register("IsChecked", typeof(bool), typeof(NavigationPaneButton),
      new UIPropertyMetadata(false, OnCheckedChanged));

  private static void OnCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   NavigationPaneButton b = d as NavigationPaneButton;
   OnCheckStateChanged(d, e);
   if ((bool)e.NewValue)
    b.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
   else
    b.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
  }

  [Category("Behavior")]
  public event RoutedEventHandler Checked
  {
   add { base.AddHandler(ToggleButton.CheckedEvent, value); }
   remove { base.RemoveHandler(ToggleButton.CheckedEvent, value); }
  }

  [Category("Behavior")]
  public event RoutedEventHandler Unchecked
  {
   add { base.AddHandler(ToggleButton.UncheckedEvent, value); }
   remove { base.RemoveHandler(ToggleButton.UncheckedEvent, value); }
  }

  /// <summary>
  /// Check strategy for click and only click events
  /// </summary>
  public NavigationPaneButtonCheckMode CheckMode
  {
   get { return (NavigationPaneButtonCheckMode)GetValue(CheckModeProperty); }
   set { SetValue(CheckModeProperty, value); }
  }
  public static readonly DependencyProperty CheckModeProperty =
      DependencyProperty.Register("CheckMode", typeof(NavigationPaneButtonCheckMode), 
      typeof(NavigationPaneButton), new UIPropertyMetadata(NavigationPaneButtonCheckMode.None,
       OnCheckStateChanged));

  public static void OnCheckStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   d.CoerceValue(DisplayStateProperty);
  }

  protected override void OnClick()
  {
   if (CheckMode == NavigationPaneButtonCheckMode.CheckUncheck ||
      (CheckMode == NavigationPaneButtonCheckMode.Check) && !IsChecked)
    IsChecked = !IsChecked;

   base.OnClick();
  }

  #endregion

  #region Anchoring

  public Dock Anchor
  {
   get { return (Dock)GetValue(AnchorProperty); }
   set { SetValue(AnchorProperty, value); }
  }
  public static readonly DependencyProperty AnchorProperty =
      DependencyProperty.Register("Anchor", typeof(Dock), typeof(NavigationPaneButton),
      new UIPropertyMetadata(Dock.Top, AnchorChanged));

  public static void AnchorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
   d.CoerceValue(IsVerticalProperty);
  }

  public bool IsVertical
  {
   get { return (bool)GetValue(IsVerticalProperty); }
  }
  protected static readonly DependencyPropertyKey IsVerticalPropertyKey =
      DependencyProperty.RegisterReadOnly("IsVertical", typeof(bool), typeof(NavigationPaneButton),
      new UIPropertyMetadata(false, null, CoerceIsVertical));
  public static readonly DependencyProperty IsVerticalProperty = IsVerticalPropertyKey.DependencyProperty;

  private static object CoerceIsVertical(DependencyObject d, object value)
  {
   Dock dock = (Dock)d.GetValue(AnchorProperty);
   return (dock == Dock.Right || dock == Dock.Left);
  }

  #endregion

  public NavigationPaneButtonDisplayState DisplayState
  {
   get { return (NavigationPaneButtonDisplayState)GetValue(DisplayStateProperty); }
  }
  protected static readonly DependencyPropertyKey DisplayStatePropertyKey =
      DependencyProperty.RegisterReadOnly("DisplayState", typeof(NavigationPaneButtonDisplayState),
      typeof(NavigationPaneButton), new UIPropertyMetadata(NavigationPaneButtonDisplayState.Normal, null, CoerceDisplayState));
  public static readonly DependencyProperty DisplayStateProperty = DisplayStatePropertyKey.DependencyProperty;

  private static object CoerceDisplayState(DependencyObject d, object value)
  {
   return (d as NavigationPaneButton).CoerceDisplayState(value);
  }

  private NavigationPaneButtonDisplayState CoerceDisplayState(object value)
  {
   // I usually do not write many comments... but this may need an explanation !
   //
   // When the button get mouse capture due to a mouse button press,
   // all mouse events are redirected to the control,
   // and the IsMouseOver DependencyProperty keeps the current value 
   // ( since you are capturing due to a MouseDownEvent the IsMouseOver == true )
   // And this value is keeped untill the user releases the mouse button
   // and the control looses the mouse capture, even if the pointer 
   // isn't under the control it self
   //
   // IsPressed Property instead is correctly update when the mouse 
   // pointer leaves the control...
   // so when mouse capture is true IsMouseOver = IsPressed !
   bool isMouseOver = IsMouseCaptured ? IsPressed : IsMouseOver;

   if (IsChecked)
   {
    if (CheckMode == NavigationPaneButtonCheckMode.Check)
     return isMouseOver ? NavigationPaneButtonDisplayState.Over :
      NavigationPaneButtonDisplayState.Checked;
    if (CheckMode == NavigationPaneButtonCheckMode.CheckUncheck)
     return isMouseOver && !IsPressed ? NavigationPaneButtonDisplayState.Checked :
      NavigationPaneButtonDisplayState.Pressed;
   }
   if (IsPressed)
    return NavigationPaneButtonDisplayState.Pressed;
   if (isMouseOver)
    return NavigationPaneButtonDisplayState.Over;

   return NavigationPaneButtonDisplayState.Normal;
  }

  public override void BeginInit()
  {
   base.BeginInit();
   DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(IsMouseOverProperty, typeof(ButtonBase));
   dpd.AddValueChanged(this, IsMouseOverChanged);
  }

  private void IsMouseOverChanged(object sender, EventArgs e)
  {
   CoerceValue(DisplayStateProperty);
   OnIsMouseOverChanged(e);
  }

  protected virtual void OnIsMouseOverChanged(EventArgs e) { }

  protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
  {
   base.OnIsPressedChanged(e);
   CoerceValue(DisplayStateProperty);
  }
 }
}
