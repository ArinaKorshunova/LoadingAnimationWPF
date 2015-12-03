using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LoadingAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LoadData> LoadDatas { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadDataToDataGrid();
        }

        private void LoadDataToDataGrid()
        {
            loadAnimation.Visibility = Visibility.Visible;
            ThreadStart dataDownloadThread = delegate
            {
                LoadDatas = GetLoadData();

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (EventHandler)
                delegate
                {
                    loadAnimation.Visibility = Visibility.Hidden;
                    clientsGrig.ItemsSource = null;
                    clientsGrig.ItemsSource = LoadDatas;
                }, null, null);
            };
            dataDownloadThread.BeginInvoke(delegate(IAsyncResult aysncResult) { dataDownloadThread.EndInvoke(aysncResult); }, null);
        }


        private ObservableCollection<LoadData> GetLoadData()
        {
            ObservableCollection<LoadData> loadData = new ObservableCollection<LoadData>();
            for (int i = 0; i < 10; i++)
            {
                loadData.Add(new LoadData
                {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    FathertName = "Иванович"
                });
                Thread.Sleep(300);
            }
            return loadData;
        }
    }
}
