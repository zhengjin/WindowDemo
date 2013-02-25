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
using Stema.Controls;
using Fluent;
using FabTab;

namespace NavigationPaneDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool NavigatioPaneOff
        {
            get { return navigationPane.Visibility == System.Windows.Visibility.Collapsed; }
            set
            {
                navigationPane.Visibility = value ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            }
        }
        //public bool ExpanderPaneOff
        //{
        //    get { return navigationPaneExpander.Visibility == System.Windows.Visibility.Collapsed; }
        //    set
        //    {
        //        navigationPaneExpander.Visibility = value ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        //    }
        //}

        private void navigationPaneExpander_CloseButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // MessageBox.Show(this.navigationPane.SelectedItemSubItems.Count().ToString()); 
            NavigationPaneItem item = new NavigationPaneItem();
            item.Header = newItemName.Text;
            item.Content = newItemContents.Text;
            StackPanel sp = new StackPanel();
            item.Content = sp;
            item.SubItems = new System.Collections.ObjectModel.ObservableCollection<NavigationPaneButton>();
            NavigationPaneButton btn = new NavigationPaneButton();
            btn.Content = "123";
            btn.Name = "实物";
            btn.Click += new RoutedEventHandler(NavPaneButton_Click);
            //item.SubItems.Insert(0, btn);
            sp.Children.Add(btn);
            btn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            NavigationPaneButton btn1 = new NavigationPaneButton();
            btn1.Content = "1232";
            btn1.Name = "成果";
            btn1.Click += new RoutedEventHandler(NavPaneButton_Click);
            //item.SubItems.Insert(0, btn);
            sp.Children.Add(btn1);
            btn.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

            item.Image = new BitmapImage(new Uri(@"pack://application:,,,/NavigationPaneDemo;component/Resources/folderopen.ico"));
            navigationPane.Items.Add(item);
            item.IsExcluded = newItemExcluded.IsChecked.Value;
        }

        protected void NavPaneButton_Click(object sender, RoutedEventArgs e)
        {
            string tabName = ((NavigationPaneButton)sender).Name.ToString();

            bool flag = false;
            if (this.tabControl.Items.Count > 0)
            {
                foreach (var mainItem in tabControl.Items)
                {
                    if (((Control)mainItem).Name == tabName)
                    {
                        tabControl.SelectedItem = mainItem;
                        flag = true;
                        return;
                    }
                }
            }
            if (!flag)
            {
                FabTabItem item = new FabTabItem();
                item.Name = tabName;
                item.Header = tabName;
                item.TabClosing+=new RoutedEventHandler(item_TabClosing);
                StackPanel s = new StackPanel();
                item.Content = s;
                s.Children.Add(new TextBlock() { Text = tabName });
                tabControl.Items.Add(item);
                tabControl.SelectedItem = item;
            }


        }

        protected void item_TabClosing(object sender, RoutedEventArgs e)
        {
            ((FabTabItem)sender).Content = null;
        }
     
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            navigationPane.SaveState(ms);
            ms.Position = 0;
            NavigationPaneDemo.Properties.Settings.Default.NavigationPaneSettings = new System.IO.StreamReader(ms).ReadToEnd();

            // Load settings 
            navigationPane.LoadState(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(NavigationPaneDemo.Properties.Settings.Default.NavigationPaneSettings)));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            navigationPane.SaveState(ms);
            ms.Position = 0;
            NavigationPaneDemo.Properties.Settings.Default.NavigationPaneSettings = new System.IO.StreamReader(ms).ReadToEnd();
            // settings are correctly saved
            //NavigationPaneDemo.Properties.Settings.Default.Reset();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Load settings 
            navigationPane.LoadState();

        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Button_Click_2(this, null);
            navigationPane.IsMinimized = true;
        }

        private void navigationPane_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

      
    }

   
}
