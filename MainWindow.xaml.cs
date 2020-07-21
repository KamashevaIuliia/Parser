using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Лаб2КамашеваПарсер
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                OnLoad();

            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Хотите загрузить файл?", "Файл для чтения не обнаружен!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        WebClient myWebClient = new WebClient();
                        myWebClient.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", Environment.CurrentDirectory + "/thrlist.xlsx");
                        OnLoad();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Ну и ладно... А зачем заходить тогда было?");
                        Application.Current.Shutdown();

                        break;
                }
                        
            }
           

        }

        public void OnLoad()
        {
            var metrics = WorkWithExcel.EnumerateMetrics(Environment.CurrentDirectory + "/thrlist.xlsx").ToList();
            MetricsDataGrid.ItemsSource = metrics;
            Style style = new Style(typeof(DataGridCell));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            MetricsDataGrid.CellStyle = style;
            foreach (var column in MetricsDataGrid.Columns)
            {
                column.Visibility = Visibility.Visible;
            }
          
           
        }
        public void OnLoadShort()
        {
            var metrics = WorkWithExcel.EnumerateMetricsShort(Environment.CurrentDirectory + "/thrlist.xlsx").ToList();
            MetricsDataGrid.ItemsSource = metrics;
            Style style = new Style(typeof(DataGridCell));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            MetricsDataGrid.CellStyle = style;
            for (int i = 2; i < MetricsDataGrid.Columns.Count; i++)
            {

                MetricsDataGrid.Columns[i].Visibility = Visibility.Collapsed;

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonRun.IsEnabled = true;
            Metric.rowscount--;
            OnLoad();
            Metric.maxrowscount = true;
            if (Metric.rowscount == 0)
            {
                ButtonReturn.IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonReturn.IsEnabled = true;
            Metric.rowscount++;
            OnLoad();
            if (Metric.maxrowscount == false)
            {
                ButtonRun.IsEnabled = false;
            }



        }

        private void ButtonShort_Click(object sender, RoutedEventArgs e)
        {
            OnLoadShort();
            ButtonShort.Visibility = Visibility.Collapsed;
            ButtonRun.Visibility = Visibility.Collapsed;
            ButtonReturn.Visibility = Visibility.Collapsed;
            ButtonShortReturn.Visibility = Visibility.Visible;
            Tyc.Visibility = Visibility.Visible;
            Name.IsReadOnly = true;
        }

        private void ButtonShortReturn_Click(object sender, RoutedEventArgs e)
        {
            Metric.rowscount = 0;
            OnLoad();
            ButtonShortReturn.Visibility = Visibility.Collapsed;
            ButtonShort.Visibility = Visibility.Visible;
            ButtonRun.Visibility = Visibility.Visible;
            ButtonReturn.Visibility = Visibility.Visible;
            Tyc.Visibility = Visibility.Collapsed;
            ButtonReturn.IsEnabled = false;
            Name.IsReadOnly = false;
        }

        private void MetricsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ButtonShortReturn.Visibility == Visibility.Visible)
            {
                var firstSelectedCellContent = this.MetricsDataGrid.Columns[0].GetCellContent(this.MetricsDataGrid.SelectedItem);
                var firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;
                string s = WorkWithExcel.Find(Environment.CurrentDirectory + "/thrlist.xlsx", Convert.ToString(firstSelectedCell).Substring(42));
                MessageBox.Show(s);
            }
            

        }
    }
}
