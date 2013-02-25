using System;
using System.Collections.Generic;
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
using Stema.Controls;

namespace NavigationPaneDemo
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
    public partial class Themes : NavigationPaneItem
    {
        public Themes()
        {
            this.InitializeComponent();
        }

        private void ChangeTheme_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ThemeManager.SetActiveTheme((NavigationPaneTheme)e.Parameter);
        }

        private void ChangeTheme_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null;
        }
    }
}