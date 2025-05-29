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

namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.NewCashiers
{
    /// <summary>
    /// Логика взаимодействия для RemoveCashierEmployeePage.xaml
    /// </summary>
    public partial class RemoveCashierEmployeePage : Page
    {
        private readonly string connectionString = "Data Source=PC-777\\SQLEXPRESS;Initial Catalog=railway_station_db;Integrated Security=True";
        private List<Cashier> cashiers = new List<Cashier>();

        private class Cashier
        {
            public int Cashiers_id { get; set; }
            public string FullName { get; set; }
            public string Login { get; set; }
        }

        public RemoveCashierEmployeePage()
        {
            InitializeComponent();
            LoadCashiers();
        }

        private void LoadCashiers(string searchTerm = "")
        {
            cashiers.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Cashiers_id, FullName, Login FROM Cashiers";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE Cashiers_id = @Id OR FullName LIKE @SearchTerm";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        if (int.TryParse(searchTerm, out int id))
                        {
                            command.Parameters.AddWithValue("@Id", id);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Id", 0); 
                        }
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cashiers.Add(new Cashier
                            {
                                Cashiers_id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Login = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            CashiersGrid.ItemsSource = null;
            CashiersGrid.ItemsSource = cashiers;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchBox.Text.Trim();
            LoadCashiers(searchTerm);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CashiersGrid.SelectedItem is Cashier selectedCashier)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить кассира {selectedCashier.FullName}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Cashiers WHERE Cashiers_id = @Id";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", selectedCashier.Cashiers_id);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Кассир успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCashiers(SearchBox.Text.Trim()); 
                }
            }
            else
            {
                MessageBox.Show("Выберите кассира для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Предыдущеё страницы нет(");
            }
        }
    }
}
