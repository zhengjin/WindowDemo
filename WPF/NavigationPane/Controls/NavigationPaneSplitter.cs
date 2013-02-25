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
using System.Windows.Controls.Primitives;

namespace Stema.Controls
{
	/// <summary>
	/// </summary>
	[
		TemplatePart(Name = NavigationPaneSplitterParts.PART_Thumb, Type = typeof(Thumb)),
	]
	public class NavigationPaneSplitter : Control
	{
		internal static class NavigationPaneSplitterParts
		{
			public const string PART_Thumb = "PART_Thumb";
		}

		static NavigationPaneSplitter()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPaneSplitter), new FrameworkPropertyMetadata(typeof(NavigationPaneSplitter)));
		}

		private Thumb part_thumb;
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			part_thumb = GetTemplateChild(NavigationPaneSplitterParts.PART_Thumb) as Thumb;
			if (part_thumb != null)
			{
				part_thumb.DragDelta += new DragDeltaEventHandler(part_thumb_DragDelta);
				part_thumb.DragStarted += new DragStartedEventHandler(part_thumb_DragStarted);
				part_thumb.DragCompleted += new DragCompletedEventHandler(part_thumb_DragCompleted);
			}

			IsInsideNavigationPane = TemplatedParent is NavigationPane;
			IsInsideNavigationPaneItem = TemplatedParent is NavigationPaneItem;
		}

		private NavigationPane navigationPane { get { return TemplatedParent as NavigationPane; } }
		private NavigationPaneItem navigationPaneItem { get { return TemplatedParent as NavigationPaneItem; } }

		bool _drag;
		private double lastLargeItemHeight;
		private double firstOverflowHeight;

		void part_thumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			_drag = false;
		}

		void part_thumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			_drag = IsInsideNavigationPane || IsInsideNavigationPaneItem;
			if (IsInsideNavigationPane)
				UpdateSizeConstraints();
		}

  private FrameworkElement GetResizeElement()
  {
   FrameworkElement el = navigationPaneItem.Content as FrameworkElement;
   if (IsInsideNavigationPaneItem && el == null)
   {
    el = (VisualTreeHelper.GetChild(navigationPaneItem, 0) as FrameworkElement);
    if (el != null)
     el = (VisualTreeHelper.GetChild(el, 0) as FrameworkElement);
   }
   return el;
  }

		void part_thumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (_drag)
			{
				if (IsInsideNavigationPane)
				{
					int enabledItems = navigationPane.Items.Count - navigationPane.ExcludedItemsCount;
					if (e.VerticalChange < -firstOverflowHeight && (navigationPane.LargeItems < enabledItems))
					{
						navigationPane.LargeItems++;
						UpdateSizeConstraints();
						e.Handled = true;
					}
					if (e.VerticalChange > lastLargeItemHeight && (navigationPane.LargeItems > 0))
					{
						navigationPane.LargeItems--;
						UpdateSizeConstraints();
						e.Handled = true;
					}
				}
				else
				{
     FrameworkElement el = GetResizeElement();
     if (el != null)
					{
						double size = el.Height + e.VerticalChange;
						el.Height = size > 0 ? size : 0;
					}
				}
			}
		}

		protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if(e.Handled == true)
				return;
			if(!IsInsideNavigationPane)
			{
				FrameworkElement el = GetResizeElement();
				if (el != null)
				{
					el.ClearValue(HeightProperty);
					el.InvalidateMeasure();
					el.InvalidateArrange();
				}
			}
		}

		#region DEPENDENCY PROPERTIES

		/// <summary>
		/// True if we are inside a NavigationPane
		/// </summary>
		public bool IsInsideNavigationPane
		{
			get { return (bool)GetValue(IsInsideNavigationPaneProperty); }
			protected set { SetValue(IsInsideNavigationPanePropertyKey, value); }
		}
		private static readonly DependencyPropertyKey IsInsideNavigationPanePropertyKey =
						DependencyProperty.RegisterReadOnly("IsInsideNavigationPane", typeof(bool), typeof(NavigationPaneSplitter),
						new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
		public static readonly DependencyProperty IsInsideNavigationPaneProperty = IsInsideNavigationPanePropertyKey.DependencyProperty;

		public bool IsInsideNavigationPaneItem
		{
			get { return (bool)GetValue(IsInsideNavigationPaneItemProperty); }
			set { SetValue(IsInsideNavigationPaneItemProperty, value); }
		}
		public static readonly DependencyProperty IsInsideNavigationPaneItemProperty =
						DependencyProperty.Register("IsInsideNavigationPaneItem", typeof(bool), typeof(NavigationPaneSplitter), new UIPropertyMetadata(false));
		
		public bool IsSimpleThumbStyle
		{
			get { return (bool)GetValue(IsSimpleThumbStyleProperty); }
			set { SetValue(IsSimpleThumbStyleProperty, value); }
		}
		public static readonly DependencyProperty IsSimpleThumbStyleProperty =
						DependencyProperty.Register("IsSimpleThumbStyle", typeof(bool), typeof(NavigationPaneSplitter), new UIPropertyMetadata(false));

		#endregion

		private void UpdateSizeConstraints()
		{
			int index = navigationPane.GetLastItemIndex(NavigationPaneItemDisplayType.Large);
   FrameworkElement item = null;

   if (index != -1)
   {
    item = navigationPane.Items[index] is NavigationPaneItem ? navigationPane.Items[index] as FrameworkElement :
     (navigationPane.ItemContainerGenerator.ContainerFromIndex(index)) as FrameworkElement;
    lastLargeItemHeight = item.RenderSize.Height;
   }
   else
    lastLargeItemHeight = 0;

			index = navigationPane.GetFirstItemIndex(NavigationPaneItemDisplayType.Small);
			if (index == -1)
				index = navigationPane.GetFirstItemIndex(NavigationPaneItemDisplayType.Undefined);

   if (index != -1)
   {
    item = navigationPane.Items[index] is NavigationPaneItem ? navigationPane.Items[index] as FrameworkElement :
    (navigationPane.ItemContainerGenerator.ContainerFromIndex(index)) as FrameworkElement;
    
    if(item != null)
     firstOverflowHeight = item.RenderSize.Height;
   }
   else
    firstOverflowHeight = 0;
		}
	}
}
