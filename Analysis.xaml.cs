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
    /// Логика взаимодействия для Analysis.xaml
    /// </summary>
    public partial class Analysis : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=School;Integrated Security=True";

        public Analysis()
        {
            InitializeComponent();

            // Заполнение ComboBox группами
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT DISTINCT GroupName FROM Students";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> groups = new List<string>();

                while (reader.Read())
                {
                    groups.Add(reader.GetString(0));
                }

                groupsComboBox.ItemsSource = groups;
            }
        }

        private void analyzeButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedGroup = groupsComboBox.SelectedItem as string;

            if (selectedGroup == null)
            {
                MessageBox.Show("Выберите группу!");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT GroupName, FirstName, LastName, PredmetName, Value
                                 FROM Ocenka
                                 INNER JOIN Students ON Ocenka.StudentId = Students.StudentId
                                 INNER JOIN Predmet ON Ocenka.PredmetId = Predmet.PredmetId
                                 WHERE GroupName = @GroupName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GroupName", selectedGroup);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                adapter.Fill(table);

 
                gradesDataGrid.ItemsSource = table.DefaultView;

                int totalGrades = table.Rows.Count;
                int goodGrades = table.AsEnumerable().Count(row => row.Field<int>("Value") >= 4);

                double percentage = (double)goodGrades / totalGrades * 100;
                percentLabel.Content = $"Процент хороших оценок: {percentage}%";
            }
        }
    }
}