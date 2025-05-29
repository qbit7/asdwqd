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
using System.Data.SqlClient;
using System.Data;


namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.TrainsManagment
{
    /// <summary>
    /// Логика взаимодействия для NewTrip_Window.xaml
    /// </summary>
    public partial class NewTrip_Window : Window
    {
        private readonly string connectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        public NewTrip_Window()
        {
            InitializeComponent();
            LoadTrainBox();
            LoadStations();
            LoadPlatforms();
            Origin_Box.SelectionChanged += Origin_Box_SelectionChanged;
            Destination_Box.SelectionChanged += Destination_Box_SelectionChanged;
        }

        private void LoadTrainBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TrainID, TrainNumber FROM Trains";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int trainId = reader.GetInt32(0);
                        string trainNumber = reader.GetString(1);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = trainNumber;
                        item.Tag = trainId;
                        TrainId_Box.Items.Add(item);
                    }
                }
            }
        }
        private void Origin_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSameStations();
        }

        private void Destination_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSameStations();
        }

        private void CheckSameStations()
        {
            if (Origin_Box.SelectedItem is ComboBoxItem originItem &&
                Destination_Box.SelectedItem is ComboBoxItem destItem)
            {
                if ((int)originItem.Tag == (int)destItem.Tag)
                {
                    MessageBox.Show("Станции отправления и назначения не могут совпадать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Destination_Box.SelectedIndex = -1;
                }
            }
        }
        private void LoadPlatforms()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT PlatformID, Number, Description FROM Platforms";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int platformId = reader.GetInt32(0);
                        int number = reader.GetInt32(1);
                        string description = reader.IsDBNull(2) ? "" : reader.GetString(2);

                        string displayText = $"{number} - {description}";

                        ComboBoxItem item = new ComboBoxItem
                        {
                            Content = displayText,
                            Tag = platformId  
                        };

                        PlatformID_Box.Items.Add(item);
                    }
                }
            }
        }



        private void LoadStations()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT StationID, StationName FROM Stations";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int stationId = reader.GetInt32(0);
                        string stationName = reader.GetString(1);

                        ComboBoxItem originItem = new ComboBoxItem { Content = stationName, Tag = stationId };
                        ComboBoxItem destinationItem = new ComboBoxItem { Content = stationName, Tag = stationId };

                        Origin_Box.Items.Add(originItem);
                        Destination_Box.Items.Add(destinationItem);
                    }
                }
            }
        }

        private void RecordNewTrip_Click(object sender, RoutedEventArgs e)
        {
            if (TrainId_Box.SelectedItem == null ||
                DepartureDate_Box.SelectedDate == null ||
                ArrivalDate_Box.SelectedDate == null ||
                Origin_Box.SelectedItem == null ||
                Destination_Box.SelectedItem == null ||
                PlatformID_Box.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int trainId = -1;
            if (TrainId_Box.SelectedItem is ComboBoxItem trainItem)
            {
                trainId = (int)trainItem.Tag;
            }

            DateTime departureTime = DepartureDate_Box.SelectedDate.Value;
            DateTime arrivalTime = ArrivalDate_Box.SelectedDate.Value;

            int originId = -1;
            if (Origin_Box.SelectedItem is ComboBoxItem originItem)
            {
                originId = (int)originItem.Tag;
            }

            int destinationId = -1;
            if (Destination_Box.SelectedItem is ComboBoxItem destinationItem)
            {
                destinationId = (int)destinationItem.Tag;
            }

            int platformId = -1;
            if (PlatformID_Box.SelectedItem is ComboBoxItem platformItem)
            {
                platformId = (int)platformItem.Tag;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = @"
            INSERT INTO Trips (TrainID, DepartureTime, ArrivalTime, Origin, Destination, PlatformID)
            VALUES (@TrainID, @DepartureTime, @ArrivalTime, 
                    (SELECT StationName FROM Stations WHERE StationID = @OriginID), 
                    (SELECT StationName FROM Stations WHERE StationID = @DestinationID), 
                    @PlatformID)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@TrainID", trainId);
                    command.Parameters.AddWithValue("@DepartureTime", departureTime);
                    command.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
                    command.Parameters.AddWithValue("@OriginID", originId);
                    command.Parameters.AddWithValue("@DestinationID", destinationId);
                    command.Parameters.AddWithValue("@PlatformID", platformId);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Маршрут успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении маршрута: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
