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
using System.Windows.Controls.Primitives;
using System.Runtime;
using System.Windows.Input;
using System.Globalization;

namespace Stema.Controls
{
 /// <summary>
 /// 
 /// </summary>
	public class NavigationPanePanel : VirtualizingPanel
	{
		private NavigationPane navigationPane { get { return TemplatedParent as NavigationPane; } }

		public NavigationPanePanel()
		{
			IsItemsHost = true;
		}

		protected override void OnVisualParentChanged(DependencyObject oldParent)
		{
			base.OnVisualParentChanged(oldParent);
			if (oldParent != null && oldParent is NavigationPane)
				(oldParent as NavigationPane).VisibleItemsChanged -= new RoutedEventHandler(NavigationPanePanel_VisibleItemsChanged);
			if (navigationPane != null)
				navigationPane.VisibleItemsChanged += new RoutedEventHandler(NavigationPanePanel_VisibleItemsChanged);
		}

		void NavigationPanePanel_VisibleItemsChanged(object sender, RoutedEventArgs e)
		{
			InvalidateMeasure();
		}

		private int GetInsertIndex(int absoluteIndex)
		{
			UIElementCollection children = InternalChildren;
			for (int j = 0; j < children.Count; j++)
				if (GetAbosluteIndex(children[j]) > absoluteIndex)
					return j;
			return children.Count;
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size desiredSize = new Size();
			UIElementCollection children = InternalChildren;
			IItemContainerGenerator generator = ItemContainerGenerator;

			int itemsCount = 0;
			if (generator != null)
			{
				int StartItemIndex = navigationPane.GetFirstItemIndex(DisplayType);
				if (StartItemIndex > -1)
				{
					int maxItems = DisplayType == NavigationPaneItemDisplayType.Large ? navigationPane.LargeItems : int.MaxValue;
					GeneratorPosition startPos = new GeneratorPosition(-1, StartItemIndex + 1);

					using (generator.StartAt(startPos, GeneratorDirection.Forward, true))
					{
						bool sizeExceeded = false;

						bool newlyRealized;
						UIElement child = null;

      // temp fix for {Disconnected Item}
      // needs more inspecting and documentation to fix it correctly !!
      // but this for now seems to work... strange items present ??
      // remove them, NOW !!!!!
      //for (int j = children.Count - 1; j > -1; j--)
      //{
       //UIElement item = children[j];
       //object o = navigationPane.ItemContainerGenerator.ItemFromContainer(item);
       //if (o == DependencyProperty.UnsetValue)
       // RemoveInternalChild(item);
      //}

						while (!sizeExceeded && (itemsCount < maxItems) && (child = generator.GenerateNext(out newlyRealized) as UIElement) != null)
						{
							bool isExcluded = NavigationPane.GetIsItemExcluded(child);
							if (!isExcluded)
							{
								if (newlyRealized || !children.Contains(child))
								{
									int absoluteIndex = StartItemIndex + itemsCount;
									int index = GetInsertIndex(absoluteIndex);

         if (absoluteIndex < 9 && navigationPane.ItemsKeyAuto)
         {
          NavigationPaneItem f = child as NavigationPaneItem;
          {
           Window w = Window.GetWindow(child);
           if (w != null)
           {
            KeyBinding binding = f.keyBinding;
            if (binding != null)
             w.InputBindings.Remove(binding);

            f.keyBinding = new KeyBinding(NavigationPane.SelectItemCommand, Key.D1 + absoluteIndex, navigationPane.ItemsKeyModifiers);
            f.keyBinding.CommandParameter = child;
            f.keyBinding.CommandTarget = navigationPane;
            w.InputBindings.Add(f.keyBinding);

            if (f != null)
             f.Gesture = (f.keyBinding.Gesture as KeyGesture).GetDisplayStringForCulture(CultureInfo.CurrentCulture);
           }
          }
         }
									SetActivePanel(child, this);
									SetAbosluteIndex(child, absoluteIndex);
									SetItemDisplayType(child, DisplayType);

									if (newlyRealized)
										generator.PrepareItemContainer(child);

         InsertInternalChild(index, child);
								}

								#region measurament algoritm
								Size childSize = new Size();
								if (Orientation == Orientation.Vertical)
									childSize = new Size(availableSize.Width, double.PositiveInfinity);
								else
									childSize = new Size(double.PositiveInfinity, availableSize.Height);

								child.Measure(childSize);

								if (Orientation == Orientation.Vertical)
								{
									sizeExceeded = desiredSize.Height + child.DesiredSize.Height > availableSize.Height;
									if (!sizeExceeded)
									{
										desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
										desiredSize.Height += child.DesiredSize.Height;
									}
								}
								else
								{
									sizeExceeded = desiredSize.Width + child.DesiredSize.Width > availableSize.Width;
									if (!sizeExceeded)
									{
										desiredSize.Width += child.DesiredSize.Width;
										desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
									}
								}
								#endregion

								if (!sizeExceeded)
									itemsCount++;
							}
							else
							{
								RemoveInternalChild(child);
							}
						}
					}
					CleanUpItems(StartItemIndex, itemsCount);
				}
			}

			if(DisplayType == NavigationPaneItemDisplayType.Small)
				navigationPane.SmallItems = itemsCount;
			return desiredSize;
		}

		private void RemoveInternalChild(UIElement child)
		{
			UIElementCollection children = InternalChildren;
			int index = children.IndexOf(child);
			if (index != -1)
				RemoveInternalChildRange(index, 1);
		}

		private void CleanUpItems(int StartItemIndex, int itemsCount)
		{
			UIElementCollection children = InternalChildren;
			IItemContainerGenerator generator = ItemContainerGenerator;

			int first = StartItemIndex;
			int last = StartItemIndex + itemsCount - 1;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				// Map a child index to an item index by going through a generator position
				GeneratorPosition childGeneratorPos = new GeneratorPosition(i, 0);
				int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPos);

				itemIndex = i + StartItemIndex;

    if (itemIndex < first || itemIndex > last)
				{
					//generator.Remove(childGeneratorPos, 1);
					//generator.Remove(new GeneratorPosition(itemIndex, 0), 1);

					RemoveInternalChildRange(i, 1);
				}
			}
		}

		private Size ArrangeChild(UIElement element, Size finalSize, Size size)
		{
			Rect r;
			Size newSize = size;

			if (Orientation == Orientation.Vertical)
			{
				r = new Rect(
					0,
					InverseAlignment ? finalSize.Height - size.Height - element.DesiredSize.Height : size.Height,
					finalSize.Width,
					element.DesiredSize.Height
					);
				newSize.Width = Math.Max(size.Width, element.DesiredSize.Width);
				newSize.Height += element.DesiredSize.Height;
			}
			else
			{
				r = new Rect(
					InverseAlignment ? finalSize.Width - size.Width - element.DesiredSize.Width : size.Width,
					0,
					element.DesiredSize.Width,
					finalSize.Height
					);
				newSize.Width += element.DesiredSize.Width;
				newSize.Height = Math.Max(size.Height, element.DesiredSize.Height);
			}
			element.Arrange(r);

			return newSize;
		}

		protected override bool HasLogicalOrientation
		{
			get { return true; }
		}

		protected override Orientation LogicalOrientation
		{
			get { return this.Orientation; }
		}

		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty OrientationProperty =
						DependencyProperty.Register("Orientation", typeof(Orientation), typeof(NavigationPanePanel), new UIPropertyMetadata(Orientation.Vertical));

		protected override Size ArrangeOverride(Size finalSize)
		{
			UIElementCollection elements = InternalChildren;
			Size size = new Size();

			if(InverseAlignment)
				for(int j = elements.Count - 1; j > -1; j--)
					size = ArrangeChild(elements[j], finalSize, size);
			else
				foreach(UIElement e in elements)
					size = ArrangeChild(e, finalSize, size);

			return finalSize;
		}

		public NavigationPaneItemDisplayType DisplayType
		{
			get { return (NavigationPaneItemDisplayType)GetValue(DisplayTypeProperty); }
			set { SetValue(DisplayTypeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemsType.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DisplayTypeProperty =
						DependencyProperty.Register("DisplayType", typeof(NavigationPaneItemDisplayType), typeof(NavigationPanePanel), new FrameworkPropertyMetadata(NavigationPaneItemDisplayType.Large));

		public bool InverseAlignment
		{
			get { return (bool)GetValue(InverseAlignmentProperty); }
			set { SetValue(InverseAlignmentProperty, value); }
		}

		// Using a DependencyProperty as the backing store for InverseInsertion.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty InverseAlignmentProperty =
						DependencyProperty.Register("InverseAlignment", typeof(bool), typeof(NavigationPanePanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange));

		public static NavigationPaneItemDisplayType GetItemDisplayType(DependencyObject obj)
		{
			return (NavigationPaneItemDisplayType)obj.GetValue(ItemDisplayTypeProperty);
		}
		internal static void SetItemDisplayType(DependencyObject obj, NavigationPaneItemDisplayType value)
		{
			obj.SetValue(ItemDisplayTypeProperty, value);
		}
		public static readonly DependencyProperty ItemDisplayTypeProperty =
      DependencyProperty.RegisterAttached("ItemDisplayType", typeof(NavigationPaneItemDisplayType), typeof(NavigationPanePanel), new UIPropertyMetadata(NavigationPaneItemDisplayType.Undefined));

		public static int GetAbosluteIndex(DependencyObject obj)
		{
			return (int)obj.GetValue(AbosluteIndexProperty);
		}
		internal static void SetAbosluteIndex(DependencyObject obj, int value)
		{
			obj.SetValue(AbosluteIndexProperty, value);
		}
		public static readonly DependencyProperty AbosluteIndexProperty =
						DependencyProperty.RegisterAttached("AbosluteIndex", typeof(int), typeof(NavigationPanePanel));

		public static NavigationPanePanel GetActivePanel(DependencyObject obj)
		{
			return (NavigationPanePanel)obj.GetValue(ActivePanelKey.DependencyProperty);
		}
		internal static void SetActivePanel(DependencyObject obj, NavigationPanePanel value)
		{
			obj.SetValue(ActivePanelKey, value);
		}
		public static readonly DependencyPropertyKey ActivePanelKey =
						DependencyProperty.RegisterAttachedReadOnly("ActivePanel", typeof(NavigationPanePanel), typeof(NavigationPanePanel),
						new PropertyMetadata(new PropertyChangedCallback(OnActivePanelChanged)));

		private static void OnActivePanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue != null)
				(e.OldValue as NavigationPanePanel).RemoveInternalChild(d as UIElement);
		}
		
	}
}
