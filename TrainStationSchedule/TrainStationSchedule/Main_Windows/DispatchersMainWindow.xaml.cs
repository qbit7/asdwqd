using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.NewCashiers;
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.TrainsManagment;

namespace TrainStationSchedule.Main_Windows
{
    /// <summary>
    /// Логика взаимодействия для DispatchersMainWindow.xaml
    /// </summary>
    public partial class DispatchersMainWindow : Window
    {
        public int DispatcherId { get; }
        public DispatchersMainWindow(int dispatcherId, string fullName)
        {
            InitializeComponent(); 
            DispatcherId = dispatcherId;
            UserName.Content = $"{fullName}";
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


        private void Add_Cashiers_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new NewCashierEmployeePage());
        }

        private void Edit_Trips_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ViewTrains());
        }


    }
}
