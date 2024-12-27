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
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        public TeachersWindow()
        {
            InitializeComponent();
        }
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            // Создаем соединение с базой данных
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=School;Integrated Security=True"))
            {
                // Открываем соединение
                connection.Open();

                // Создаем команду для выполнения запроса
                SqlCommand command = new SqlCommand("SELECT * FROM Prepod WHERE FirstName LIKE @FirstName AND Post LIKE @Post", connection);

                // Добавляем параметры для запроса
                command.Parameters.AddWithValue("@FirstName", "%" + tbFirstName.Text + "%");
                command.Parameters.AddWithValue("@Post", "%" + tbPosition.Text + "%");

                // Создаем адаптер данных для получения результатов запроса
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                // Создаем таблицу для заполнения данными из базы данных
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Заполняем таблицу в интерфейсе данными из таблицы в памяти
                dgTeachers.ItemsSource = table.DefaultView;
            }
        }
    }
}
