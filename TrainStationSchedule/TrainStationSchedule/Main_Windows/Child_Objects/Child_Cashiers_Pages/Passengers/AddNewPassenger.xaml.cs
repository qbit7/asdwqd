using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers
{
    /// <summary>
    /// Логика взаимодействия для AddNewPassenger.xaml
    /// </summary>
    public partial class AddNewPassenger : Window
    {
        public AddNewPassenger()
        {
            InitializeComponent();
        }
        private readonly string connectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        private void AddPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameBox.Text.Trim();
            string passport = PassportBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(passport) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var fioPattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+( [А-ЯЁ][а-яё]+)?$";
            if (!Regex.IsMatch(fullName, fioPattern))
            {
                MessageBox.Show("ФИО должно быть в формате: Иванов Иван (или Иванов Иван Иванович).");
                return;
            }

            var passportPattern = @"^\d{4} \d{6}$";
            if (!Regex.IsMatch(passport, passportPattern))
            {
                MessageBox.Show("Паспорт должен быть в формате: 1234 567890");
                return;
            }

            var phonePattern = @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$";
            if (!Regex.IsMatch(phone, phonePattern))
            {
                MessageBox.Show("Телефон должен быть в формате: +7 (999) 999-99-99");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Passengers WHERE PassportNumber = @passport", conn);
                checkCmd.Parameters.AddWithValue("@passport", passport);

                int exists = (int)checkCmd.ExecuteScalar();
                if (exists > 0)
                {
                    MessageBox.Show("Пассажир с таким номером паспорта уже существует.");
                    return;
                }


                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO Passengers (FullName, PassportNumber, Phone) VALUES (@fullName, @passport, @phone)", conn);
                insertCmd.Parameters.AddWithValue("@fullName", fullName);
                insertCmd.Parameters.AddWithValue("@passport", passport);
                insertCmd.Parameters.AddWithValue("@phone", phone);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Пассажир успешно добавлен!");
                FullNameBox.Clear();
                PassportBox.Clear();
                PhoneBox.Clear();
            }
        }

        private void PassportBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9 ]");
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9+()\\- ]");
        }

        private void AddPassenger_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameBox.Text.Trim();
            string passport = PassportBox.Text.Replace(" ", "").Trim();
            string phone = PhoneBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(passport) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!Regex.IsMatch(fullName, @"^([А-ЯЁ][а-яё]+ ){1,2}[А-ЯЁ][а-яё]+$"))
            {
                MessageBox.Show("ФИО должно быть в формате 'Иванов Иван' или 'Иванов Иван Иванович'.");
                return;
            }

            if (passport.Length != 10 || !passport.All(char.IsDigit))
            {
                MessageBox.Show("Неверный формат паспорта.");
                return;
            }

            if (!Regex.IsMatch(phone, @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Неверный формат номера телефона.");
                return;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Passengers WHERE PassportNumber = @Passport", connection);
                checkCmd.Parameters.AddWithValue("@Passport", passport);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Пассажир с таким паспортом уже существует.");
                    return;
                }

                var insertCmd = new SqlCommand("INSERT INTO Passengers (FullName, PassportNumber, Phone) VALUES (@FullName, @Passport, @Phone)", connection);
                insertCmd.Parameters.AddWithValue("@FullName", fullName);
                insertCmd.Parameters.AddWithValue("@Passport", passport);
                insertCmd.Parameters.AddWithValue("@Phone", phone);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Пассажир добавлен успешно!");
            }
        }

    }

}
