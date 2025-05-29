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
using TrainStationSchedule.Main_Windows;
using System.Data.SqlClient;

namespace TrainStationSchedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private readonly string connectionString = @"Data Source=PC-777\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string dispatcherQuery = "SELECT Dispatcher_id, FullName FROM Dispatchers WHERE Login = @Login AND Password = @Password";
                using (SqlCommand command = new SqlCommand(dispatcherQuery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string fullName = reader.GetString(1);
                            DispatchersMainWindow dispatchersMainWindow = new DispatchersMainWindow(id, fullName);
                            dispatchersMainWindow.Show();
                            this.Close();
                            return;
                        }
                    }
                }

                string cashierQuery = "SELECT Cashiers_id, FullName FROM Cashiers WHERE Login = @Login AND Password = @Password";
                using (SqlCommand command = new SqlCommand(cashierQuery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string fullName = reader.GetString(1);
                            CashiersMainWindow cashierWindow = new CashiersMainWindow(id, fullName);
                            cashierWindow.Show();
                            this.Close();
                            return;
                        }
                    }
                }
            }

            MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
