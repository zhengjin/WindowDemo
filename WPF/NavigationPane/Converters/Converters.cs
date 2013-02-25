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
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stema.Controls.Converters
{
	[ValueConversion(typeof(Visibility), typeof(bool))]
	public class BooleanToVisibilityConverter : IValueConverter
	{
		#region IValueConverter Members

		private bool IsHideOnly(List<string> parameters)
		{
			return parameters.Contains("hideonly");
		}

		private bool IsInverse(List<string> parameters)
		{
			return parameters.Contains("inverse");
		}

		private List<string> GetParameters(object parameter)
		{
			List<string> pars = new List<string>();
			if (parameter != null)
				pars.AddRange(((string)parameter).ToLower().Split(','));

			return pars;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			List<string> pars = GetParameters(parameter);
			bool isHideOnly = IsHideOnly(pars);

			if (value is bool)
			{
				bool val = (bool)value;
				if (IsInverse(pars))
					val = !val;
				
				if (val)
					return Visibility.Collapsed;
				return isHideOnly ? Visibility.Hidden : Visibility.Visible;
			}
			else if (value is Visibility)
				return ConvertBack(value, targetType, parameter, culture);

			return value;
		}

		private Visibility InverseVisibility(Visibility value, bool isHideOnly)
		{
			Visibility result = value;
			if (value == Visibility.Collapsed || (!isHideOnly && value == Visibility.Hidden))
				return Visibility.Visible;
			else
				return isHideOnly ? Visibility.Hidden : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			List<string> pars = GetParameters(parameter);
			bool isHideOnly = IsHideOnly(pars);

			if (value is Visibility)
			{
				Visibility v = (Visibility)value;
				if (IsInverse(pars))
					v = InverseVisibility(v, isHideOnly);
				
				if (v == Visibility.Visible || (v == Visibility.Hidden && isHideOnly))
					return true;
				return false;
			}
			else if (value is bool)
				return Convert(value, targetType, parameter, culture);

			return value;
		}

		#endregion
	}

	//[ValueConversion(typeof(Visibility), typeof(bool)) ]
	//public class BooleanToVisibilityConverterInverse: IValueConverter
	//{
	// #region IValueConverter Members

	// public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  if ((bool)value)
	//   return (string)parameter == "HideOnly" ? Visibility.Hidden : Visibility.Collapsed;
	//  return Visibility.Visible;
	// }

	// public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  if ((Visibility)value == Visibility.Visible)
	//   return false;
	//  return true;
	// }

	// #endregion
	//}

	[ValueConversion(typeof(bool), typeof(bool))]
	public class BooleanNegateConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}

		#endregion
	}

	//public sealed class GripAlignmentConverter : IValueConverter
	//{
	// public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  Orientation orientation = (Orientation)parameter;
	//  ResizeDirection resizeDirection = (ResizeDirection)value;

	//  switch (orientation)
	//  {
	//   case Orientation.Horizontal:
	//    if (resizeDirection == ResizeDirection.NorthEast || resizeDirection == ResizeDirection.SouthEast)
	//    {
	//     return HorizontalAlignment.Right;
	//    }
	//    else
	//    {
	//     return HorizontalAlignment.Left;
	//    }
	//   case Orientation.Vertical:
	//    if (resizeDirection == ResizeDirection.NorthEast || resizeDirection == ResizeDirection.NorthWest)
	//    {
	//     return VerticalAlignment.Top;
	//    }
	//    else
	//    {
	//     return VerticalAlignment.Bottom;
	//    }
	//  }

	//  return DependencyProperty.UnsetValue;
	// }

	// public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  return DependencyProperty.UnsetValue;
	// }
	//}

	//public sealed class GripCursorConverter : IValueConverter
	//{
	// public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  ResizeDirection resizeDirection = (ResizeDirection)value;

	//  switch (resizeDirection)
	//  {
	//   case ResizeDirection.NorthEast:
	//   case ResizeDirection.SouthWest:
	//    return System.Windows.Input.Cursors.SizeNESW;
	//   case ResizeDirection.NorthWest:
	//   case ResizeDirection.SouthEast:
	//    return System.Windows.Input.Cursors.SizeNWSE;
	//  }

	//  return DependencyProperty.UnsetValue;
	// }

	// public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  return DependencyProperty.UnsetValue;
	// }
	//}

	//public sealed class GripRotationConverter : IValueConverter
	//{
	// public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  ResizeDirection resizeDirection = (ResizeDirection)value;

	//  switch (resizeDirection)
	//  {
	//   case ResizeDirection.SouthWest:
	//    return 90;
	//   case ResizeDirection.NorthWest:
	//    return 180;
	//   case ResizeDirection.NorthEast:
	//    return 270;
	//  }

	//  return 0;
	// }

	// public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	// {
	//  return DependencyProperty.UnsetValue;
	// }
	//}


	[ValueConversion(typeof(ImageSource), typeof(Image))]
	public class ImageSourceToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Image image = null;
			ImageSource imageSource = value as ImageSource;
			if (imageSource != null)
			{
				image = new Image();
				image.SnapsToDevicePixels = true;
				double size;
				if (parameter != null && double.TryParse((string)parameter, out size))
					image.Width = image.Height = size;

				image.Source = imageSource;
			}
			return image;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
