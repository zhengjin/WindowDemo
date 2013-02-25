#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Stema.Utils;

namespace Stema.Controls
{
	/// <summary>
	/// Interaction logic for NavigationPaneOptions.xaml
	/// </summary>
	public partial class NavigationPaneOptions : Window
	{
		internal class NavigationPaneOptionsData
		{
			object header;

			public object Header
			{
				get { return header; }
				set { header = value; }
			}
			bool isNotExcluded;

			public bool IsNotExcluded
			{
				get { return isNotExcluded; }
				set { isNotExcluded = value; }
			}

			internal object item;

			public NavigationPaneOptionsData(object item, object header, bool isNotExcluded)
			{
				this.item = item;
				this.header = header;
				this.isNotExcluded = isNotExcluded;
			}
		}



  public bool CanReorder
  {
   get { return (bool)GetValue(CanReorderProperty); }
  }
  public static readonly DependencyPropertyKey CanReorderPropertyKey =
      DependencyProperty.RegisterReadOnly("CanReorder", typeof(bool), typeof(NavigationPaneOptions), new UIPropertyMetadata(false, null, CoerceCanReorder));
  public static readonly DependencyProperty CanReorderProperty = CanReorderPropertyKey.DependencyProperty;

  private static object CoerceCanReorder(DependencyObject d, object value)
  {
   NavigationPaneOptions w = d as NavigationPaneOptions;
   return w.navigationPane.ItemsSource == null;
  }
  
		
		internal ObservableCollection<NavigationPaneOptionsData> Items;

		private NavigationPane navigationPane;
		public NavigationPaneOptions(NavigationPane navigationPane)
		{
			this.navigationPane = navigationPane;
   CoerceValue(CanReorderProperty);
			Items = new ObservableCollection<NavigationPaneOptionsData>();
			ResetItems();
			this.InitializeComponent();
      
			DataContext = Items;
		}

		private void ResetItems()
		{
			Items.Clear();
			for (int j = 0; j < navigationPane.Items.Count; j++)
			{
    NavigationPaneItem item = navigationPane.Items[j] is NavigationPaneItem ? navigationPane.Items[j] as NavigationPaneItem:
     navigationPane.ItemContainerGenerator.ContainerFromIndex(j) as NavigationPaneItem;
				if(item != null)
					Items.Add(new NavigationPaneOptionsData(item, XamlHelper.CloneUsingXaml(item.Header, true), !NavigationPane.GetIsItemExcluded(item)));
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
   if (!CanReorder)
   {
    e.CanExecute = false;
    return;
   }

			if (itemsListBox != null && itemsListBox.SelectedIndex > -1)
			{
				if (e.Source == up)
					e.CanExecute = itemsListBox.SelectedIndex > 0;
				if (e.Source == down)
					e.CanExecute = itemsListBox.SelectedIndex < itemsListBox.Items.Count - 1;
			}
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			object selectedItem = itemsListBox.SelectedItem;

			int itemIndex = itemsListBox.SelectedIndex;
			int swapIndex = itemIndex + (e.Source == up ? -1 : 1);

			NavigationPaneOptionsData item = Items[Math.Min(itemIndex, swapIndex)];
			Items.Remove(item);
			Items.Insert(Math.Max(itemIndex, swapIndex), item);

			itemsListBox.SelectedItem = selectedItem;
			itemsListBox.Focus();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			ResetItems();
		}


		[DllImport("user32.dll")]
		static extern int GetWindowLong(IntPtr hwnd, int index);
		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);
		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

		const int GWL_STYLE = -16;
		const int WS_MINIMIZE = 0x20000000;
		const int WS_MAXIMIZE = 0x01000000;
		const int WS_MINIMIZEBOX = 0x00020000;
		const int WS_MAXIMIZEBOX = 0x00010000;

		const int GWL_EXSTYLE = -20;
		const int WS_EX_DLGMODALFRAME = 0x0001;
		const uint WS_EX_CONTEXTHELP = 0x400;

		const int SWP_NOSIZE = 0x0001;
		const int SWP_NOMOVE = 0x0002;
		const int SWP_NOZORDER = 0x0004;
		const int SWP_FRAMECHANGED = 0x0020;
		const uint WM_SETICON = 0x0080;

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);


			//Stefano:  quello che segue significa aver programmato per windows in c !!!!

			// in wpf non si può mettere il bottone help nella titlebar !!!
			// neanche con gli styles !!
			// perchè finche c'è la title bar e gestita dal gdi !!!

			// come fare ? 
			// se è gestita dal gdi allora risponde alle api di windows !
			// usiamo GetWindowLong e set windowLong per cambiare lo style ( WS_ e WS_EX ) della window

			// prende l'hwnd della finestra
			IntPtr hwnd = new WindowInteropHelper(this).Handle;

			int style = GetWindowLong(hwnd, GWL_STYLE);
			SetWindowLong(hwnd, GWL_STYLE, (int)(style & ~(WS_MINIMIZEBOX | WS_MAXIMIZEBOX)));

			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			SetWindowLong(hwnd, GWL_EXSTYLE, (int)(extendedStyle | WS_EX_DLGMODALFRAME));

			// aggiorna la non client area della finestra per rispecchiare le modifiche
			SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
		}
	}
}