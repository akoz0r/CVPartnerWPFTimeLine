using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CVGuru.Domain;
using CVGuru.ViewModel;
using Telerik.Windows.Controls;

namespace CVGuru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Search();
        }

        private void Dummy_OnClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).LoadDummyData();
        }
    }
}
