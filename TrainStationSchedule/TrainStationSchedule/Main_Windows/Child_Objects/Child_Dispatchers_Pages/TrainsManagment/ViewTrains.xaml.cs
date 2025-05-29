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
using System.Data.SqlClient;
using System.Data;

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.TrainsManagment
{
    /// <summary>
    /// Логика взаимодействия для ViewTrains.xaml
    /// </summary>
    public partial class ViewTrains : Page
    {
        private readonly string connectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        public ViewTrains()
        {
            InitializeComponent();
            LoadTrips();
            OriginColumn.ItemsSource = stationsTable.DefaultView;
            DestinationColumn.ItemsSource = stationsTable.DefaultView;

            platformsTable.Columns.Add("PlatformDisplay", typeof(string));
            foreach (DataRow row in platformsTable.Rows)
            {
                row["PlatformDisplay"] = $"{row["StationID"]} - {row["Description"]}";
            }

            PlatformColumn.ItemsSource = platformsTable.DefaultView;

        }
        private DataTable stationsTable;
        private DataTable platformsTable;

        private void LoadTrips()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryTrips = "SELECT TripID, TrainID, DepartureTime, ArrivalTime, Origin, Destination, PlatformID FROM Trips";
                SqlDataAdapter adapter = new SqlDataAdapter(queryTrips, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                TripsGrid.ItemsSource = dt.DefaultView;

                SqlDataAdapter stationsAdapter = new SqlDataAdapter("SELECT StationName FROM Stations", connection);
                stationsTable = new DataTable();
                stationsAdapter.Fill(stationsTable);

                SqlDataAdapter platformsAdapter = new SqlDataAdapter("SELECT PlatformID, StationID, Description FROM Platforms", connection);
                platformsTable = new DataTable();
                platformsAdapter.Fill(platformsTable);
            }
        }



        private void DeleteTrip_Click(object sender, RoutedEventArgs e)
        {
            if (TripsGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите рейс для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var rowView = TripsGrid.SelectedItem as DataRowView;
            if (rowView == null)
            {
                MessageBox.Show("Ошибка выбора рейса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int tripId = Convert.ToInt32(rowView["TripID"]);

            var result = MessageBox.Show($"Вы уверены, что хотите удалить рейс ID={tripId}?",
                                         "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Trips WHERE TripID = @TripID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TripID", tripId);

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Рейс успешно удалён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadTrips();
                            }
                            else
                            {
                                MessageBox.Show("Рейс не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при удалении рейса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void TripsGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                DataRowView rowView = e.Row.Item as DataRowView;
                if (rowView != null)
                {

                    int tripId = Convert.ToInt32(rowView["TripID"]);
                    int trainId = Convert.ToInt32(rowView["TrainID"]);
                    DateTime departureTime = Convert.ToDateTime(rowView["DepartureTime"]);
                    DateTime arrivalTime = Convert.ToDateTime(rowView["ArrivalTime"]);
                    string origin = rowView["Origin"].ToString();
                    string destination = rowView["Destination"].ToString();

                    if (origin == destination)
                    {
                        MessageBox.Show("Пункт отправления и назначения не могут быть одинаковыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        e.Cancel = true;
                        LoadTrips();
                        return;
                    }

                    int platformId = Convert.ToInt32(rowView["PlatformID"]);



                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = @"
                    UPDATE Trips
                    SET TrainID = @TrainID,
                        DepartureTime = @DepartureTime,
                        ArrivalTime = @ArrivalTime,
                        Origin = @Origin,
                        Destination = @Destination,
                        PlatformID = @PlatformID
                    WHERE TripID = @TripID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@TrainID", trainId);
                            command.Parameters.AddWithValue("@DepartureTime", departureTime);
                            command.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
                            command.Parameters.AddWithValue("@Origin", origin);
                            command.Parameters.AddWithValue("@Destination", destination);
                            command.Parameters.AddWithValue("@PlatformID", platformId);
                            command.Parameters.AddWithValue("@TripID", tripId);

                            try
                            {
                                command.ExecuteNonQuery();
                                MessageBox.Show("Рейс успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при обновлении рейса: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }




        private void AddNewTrip_Click(object sender, RoutedEventArgs e)
        {
            NewTrip_Window newTrip_Window = new NewTrip_Window();
            newTrip_Window.Show();
        }
    }
}
