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
using TrainStationSchedule;

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.NewCashiers
{
    /// <summary>
    /// Логика взаимодействия для NewCashierEmployeePage.xaml
    /// </summary>
    public partial class NewCashierEmployeePage : Page
    {
        private readonly string connectionString = "Data Source=PC-777\\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";

        public NewCashierEmployeePage()
        {
            InitializeComponent();
        }

        private void AddCashierButton_Click(object sender, RoutedEventArgs e)
        {
            string surname = CashierSurNameBox.Text.Trim();
            string name = CashierNameBox.Text.Trim();
            string patronymic = CashierPatronymicBox.Text.Trim();
            string login = InputLoginBox.Text.Trim();
            string password = InputPasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все обязательные поля (*).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (login.Length < 5)
            {
                MessageBox.Show("Логин должен содержать не менее 5 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string fullName = $"{surname} {name}";
            if (!string.IsNullOrEmpty(patronymic))
            {
                fullName += $" {patronymic}";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM Cashiers WHERE Login = @Login";
                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Кассир с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Cashiers (FullName, Login, Password) VALUES (@FullName, @Login, @Password)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Новый кассир успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            CashierSurNameBox.Text = string.Empty;
            CashierNameBox.Text = string.Empty;
            CashierPatronymicBox.Text = string.Empty;
            InputLoginBox.Text = string.Empty;
            InputPasswordBox.Password = string.Empty;
        }

        private void RemoveCashierButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RemoveCashierEmployeePage());
        }
    }
}
