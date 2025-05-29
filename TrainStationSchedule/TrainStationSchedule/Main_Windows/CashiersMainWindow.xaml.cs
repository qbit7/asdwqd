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
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers;
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Tickets;


namespace TrainStationSchedule.Main_Windows
{
    /// <summary>
    /// Логика взаимодействия для CashiersMainWindow.xaml
    /// </summary>
    public partial class CashiersMainWindow : Window
    {
        public int CashierId { get; }
        public CashiersMainWindow(int cashierId, string fullName)
        {
            InitializeComponent();
            CashierId = cashierId;
            UserName.Content = $"{fullName}";
        }

        private void Tickets_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Sell_Ticket_Page());
        }

        private void Passengers_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new View_Passengers());
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}