using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Student
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow()
        {
            InitializeComponent();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения полей для поиска
            string lastName = searchLastName.Text;
            string groupName = searchGroup.Text;

            // Формируем SQL-запрос с учетом фильтров
            string query = "SELECT * FROM Students WHERE 1=1";

            if (!string.IsNullOrEmpty(lastName))
            {
                query += $" AND LastName LIKE '{lastName}%'";
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                query += $" AND GroupName = '{groupName}'";
            }

            // Выполняем запрос к базе данных
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=School;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                // Создаем таблицу в памяти и заполняем ее данными из базы данных
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Привязываем таблицу к DataGrid
                studentsGrid.ItemsSource = table.DefaultView;
            }
        }
    }


     
}
