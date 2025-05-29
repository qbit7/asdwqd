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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers
{
    /// <summary>
    /// Логика взаимодействия для View_Passengers.xaml
    /// </summary>
    public partial class View_Passengers : Page
    {
        public View_Passengers()
        {
            InitializeComponent();
            LoadPassengers();
        }
        private readonly string connectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";

        private void LoadPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PassengerID, FullName, PassportNumber, Phone FROM Passengers";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    passengers.Add(new Passenger
                    {
                        PassengerID = (int)reader["PassengerID"],
                        FullName = reader["FullName"].ToString(),
                        PassportNumber = reader["PassportNumber"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
            }

            PassengersGrid.ItemsSource = passengers;
        }

        private void DeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (PassengersGrid.SelectedItem is Passenger selectedPassenger)
            {
                MessageBoxResult result = MessageBox.Show($"Удалить пассажира {selectedPassenger.FullName}?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Passengers WHERE PassengerID = @PassengerID";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@PassengerID", selectedPassenger.PassengerID);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadPassengers();
                }
            }
        }

        private void AddPassenger_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassenger addNewPassenger = new AddNewPassenger();
            addNewPassenger.Show();
        }
    }

    public class Passenger
    {
        public int PassengerID { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string Phone { get; set; }
    }

}
