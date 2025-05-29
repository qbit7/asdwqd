using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers;

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Tickets
{
    public partial class Sell_Ticket_Page : Page
    {
        public Sell_Ticket_Page()
        {
            InitializeComponent();
            LoadTrips();
        }

        private readonly string SqlConnectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        private int? CurrentPassengerID = null;
        private int? selectedTripID = null;
        public int TripID { get; set; }
        private const decimal BasePrice = 500;

        public class TripItem
        {
            public int TripID { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        private void LoadTrips()
        {
            try
            {
                List<TripItem> trips = new List<TripItem>();

                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();
                    string query = "SELECT TripID, DepartureTime, ArrivalTime, Origin, Destination FROM Trips";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int tripId = reader.GetInt32(0);
                            DateTime departureTime = reader.GetDateTime(1);
                            DateTime arrivalTime = reader.GetDateTime(2);
                            string origin = reader.GetString(3);
                            string destination = reader.GetString(4);

                            string display = $"ID:{tripId}. {departureTime:dd.MM} → {arrivalTime:dd.MM}. {origin} → {destination}";

                            trips.Add(new TripItem
                            {
                                TripID = tripId,
                                DisplayText = display
                            });
                        }
                    }
                }

                if (trips.Count == 0)
                {
                    MessageBox.Show("Маршруты не найдены в базе данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                TripComboBox.ItemsSource = trips;
                TripComboBox.SelectedIndex = -1;
                selectedTripID = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке маршрутов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rawPassport = PassportBox.Text.Replace(" ", "");

                if (rawPassport.Length != 10)
                {
                    MessageBox.Show("Введите паспортные данные в корректном формате (например, 9999 999999).", "Неверный формат", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();
                    string query = "SELECT PassengerID, FullName FROM Passengers WHERE PassportNumber = @PassportNumber";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PassportNumber", rawPassport);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int passengerId = reader.GetInt32(0);
                                string fullName = reader.GetString(1);

                                CurrentPassengerID = passengerId;
                                ClientNameLabel.Content = fullName;
                            }
                            else
                            {
                                MessageBoxResult res = MessageBox.Show(
                                    "Такого пользователя ещё нет в БД. Добавить его?",
                                    "Пользователь не найден",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question);

                                if (res == MessageBoxResult.Yes)
                                {
                                    AddNewPassenger addWindow = new AddNewPassenger();
                                    addWindow.ShowDialog();
                                }
                                else
                                {
                                    PassportBox.Text = string.Empty;
                                }

                                ClientNameLabel.Content = "-";
                                CurrentPassengerID = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске пассажира: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewPassenger_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassenger addWindow = new AddNewPassenger();
            addWindow.ShowDialog();
        }

        private void TripComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TripComboBox.SelectedItem is TripItem selectedTrip)
            {
                selectedTripID = selectedTrip.TripID;
                ValidateSeatNumbers();
            }
            else
            {
                selectedTripID = null;
                ValidateSeatNumbers();
            }
        }

        private void SeatNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateSeatNumbers();
        }

        private bool ValidateSeatNumbers()
        {
            try
            {
                string input = SeatNumberBox.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    SeatNumberBox.Background = System.Windows.Media.Brushes.White;
                    PriceBox.Text = BasePrice.ToString("F0");
                    return false;
                }

                string[] seatInputs = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> seatNumbers = new List<int>();

                foreach (string seat in seatInputs)
                {
                    if (!int.TryParse(seat.Trim(), out int seatNumber))
                    {
                        MessageBox.Show($"Номер места '{seat}' не является числом.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                        SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                        PriceBox.Text = BasePrice.ToString("F0");
                        return false;
                    }

                    if (seatNumber < 1 || seatNumber > 450)
                    {
                        MessageBox.Show($"Номер места '{seatNumber}' должен быть в диапазоне от 1 до 450.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                        SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                        PriceBox.Text = BasePrice.ToString("F0");
                        return false;
                    }

                    seatNumbers.Add(seatNumber);
                }

                if (seatNumbers.Count == 0)
                {
                    SeatNumberBox.Background = System.Windows.Media.Brushes.White;
                    PriceBox.Text = BasePrice.ToString("F0");
                    return false;
                }

                if (seatNumbers.Count != seatNumbers.Distinct().Count())
                {
                    MessageBox.Show("Введены повторяющиеся номера мест. Каждое место должно быть уникальным.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                    PriceBox.Text = BasePrice.ToString("F0");
                    return false;
                }

                if (!selectedTripID.HasValue)
                {
                    MessageBox.Show("Пожалуйста, выберите маршрут.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                    PriceBox.Text = BasePrice.ToString("F0");
                    return false;
                }

                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();
                    foreach (int seatNumber in seatNumbers)
                    {
                        string query = "SELECT COUNT(*) FROM Tickets WHERE TripID = @TripID AND SeatNumber = @SeatNumber";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@TripID", selectedTripID.Value);
                            command.Parameters.AddWithValue("@SeatNumber", seatNumber);

                            int count = (int)command.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show($"Место {seatNumber} уже занято для выбранного маршрута.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                                SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                                PriceBox.Text = BasePrice.ToString("F0");
                                return false;
                            }
                        }
                    }
                }

                decimal totalPrice = BasePrice * seatNumbers.Count;
                PriceBox.Text = totalPrice.ToString("F0");
                SeatNumberBox.Background = System.Windows.Media.Brushes.LightGreen;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке мест: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                SeatNumberBox.Background = System.Windows.Media.Brushes.LightPink;
                PriceBox.Text = BasePrice.ToString("F0");
                return false;
            }
        }

        private void SellTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CurrentPassengerID.HasValue)
                {
                    MessageBox.Show("Пожалуйста, выберите пассажира.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!selectedTripID.HasValue)
                {
                    MessageBox.Show("Пожалуйста, выберите маршрут.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!ValidateSeatNumbers())
                {
                    return;
                }

                string input = SeatNumberBox.Text.Trim();
                string[] seatInputs = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> seatNumbers = seatInputs.Select(s => int.Parse(s.Trim())).ToList();

                string clientName = ClientNameLabel.Content.ToString();
                string passportNumber = PassportBox.Text;
                string tripText = (TripComboBox.SelectedItem as TripItem)?.DisplayText ?? "Не выбрано";
                string seats = string.Join(", ", seatNumbers);
                string price = PriceBox.Text;
                string paymentMethod = PaymentMethodBox.SelectedItem is ComboBoxItem item ? item.Content.ToString() : "Не выбрано";

                string confirmationMessage =
                    "Подтвердить продажу?\n\n" +
                    $"Клиент: {clientName}\n" +
                    $"Номер паспорта: {passportNumber}\n" +
                    $"Маршрут: {tripText}\n" +
                    $"Места: {seats}\n" +
                    $"Стоимость: {price} руб.\n" +
                    $"Способ оплаты: {paymentMethod}";

                MessageBoxResult result = MessageBox.Show(
                    confirmationMessage,
                    "Подтверждение продажи",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    PassportBox.Text = "";
                    ClientNameLabel.Content = "-";
                    CurrentPassengerID = null;
                    SeatNumberBox.Text = "";
                    TripComboBox.SelectedIndex = -1;
                    selectedTripID = null;
                    PaymentMethodBox.SelectedIndex = -1;
                    PriceBox.Text = BasePrice.ToString("F0");
                    SeatNumberBox.Background = System.Windows.Media.Brushes.White;
                    return;
                }

                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();
                    foreach (int seatNumber in seatNumbers)
                    {
                        string query = @"
                            INSERT INTO Tickets (TripID, PassengerID, SeatNumber, Price, PurchaseDate, PaymentMethod)
                            VALUES (@TripID, @PassengerID, @SeatNumber, @Price, @PurchaseDate, @PaymentMethod)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@TripID", selectedTripID.Value);
                            command.Parameters.AddWithValue("@PassengerID", CurrentPassengerID.Value);
                            command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                            command.Parameters.AddWithValue("@Price", BasePrice);
                            command.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                            command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show($"Билет(ы) успешно оформлен(ы) для мест: {seats}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                PassportBox.Text = "";
                ClientNameLabel.Content = "-";
                CurrentPassengerID = null;
                SeatNumberBox.Text = "";
                TripComboBox.SelectedIndex = -1;
                selectedTripID = null;
                PaymentMethodBox.SelectedIndex = -1;
                PriceBox.Text = BasePrice.ToString("F0");
                SeatNumberBox.Background = System.Windows.Media.Brushes.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении билета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}