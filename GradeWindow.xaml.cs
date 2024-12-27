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
    /// Логика взаимодействия для GradeWindow.xaml
    /// </summary>
    public partial class GradeWindow : Window
    {
        private readonly string connectionString = @"Data Source=localhost;Initial Catalog=School;Integrated Security=True";

        public GradeWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // запрос для получения данных из связанных таблиц
                string query = @"SELECT Students.FirstName , Students.GroupName, Students.LastName, Predmet.PredmetName, Ocenka.Value
                                 FROM Ocenka
                                 INNER JOIN Students ON Students.StudentId = Ocenka.StudentId
                                 INNER JOIN Predmet ON Predmet.PredmetId = Ocenka.PredmetId";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
            }

            gradesDataGrid.ItemsSource = dt.DefaultView;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=School;Integrated Security=True";

            string firstname = firstnameTextBox.Text;
            string group = groupTextBox.Text;
            string subject = predmetTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string groupName = groupNameTextBox.Text;
            int? minGrade = null;
            int? maxGrade = null;

            if (!string.IsNullOrEmpty(minGradeTextBox.Text))
                minGrade = int.Parse(minGradeTextBox.Text);
            if (!string.IsNullOrEmpty(maxGradeTextBox.Text))
                maxGrade = int.Parse(maxGradeTextBox.Text);

            string query = "SELECT Students.FirstName, Students.LastName, Students.GroupName, Predmet.PredmetName, Ocenka.Value " +
                           "FROM Ocenka " +
                           "INNER JOIN Students ON Ocenka.StudentID = Students.Studentid " +
                           "INNER JOIN Predmet ON Ocenka.PredmetID = Predmet.Predmetid " +
                           "WHERE (@FirstName = '' OR Students.FirstName LIKE '%' + @FirstName + '%') " +
                           "AND (@Group = '' OR Students.GroupName = @Group) " +
                           "AND (@PredmetName = '' OR Predmet.PredmetName LIKE '%' + @PredmetName + '%') " +
                           "AND (@LastName = '' OR Students.LastName LIKE '%' + @LastName + '%') " +
                           "AND (@GroupName = '' OR Students.GroupName LIKE '%' + @GroupName + '%') " +
                           "AND (@MinGrade IS NULL OR Ocenka.Value >= @MinGrade) " +
                           "AND (@MaxGrade IS NULL OR Ocenka.Value <= @MaxGrade)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstname);
                command.Parameters.AddWithValue("@Group", group);
                command.Parameters.AddWithValue("@PredmetName", subject);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@GroupName", groupName);
                command.Parameters.AddWithValue("@MinGrade", minGrade);
                command.Parameters.AddWithValue("@MaxGrade", maxGrade);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gradesDataGrid.ItemsSource = table.DefaultView;
            }
        }
        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // запрос для добавления записи в таблицу оценок

                 string query = @"INSERT INTO Ocenka (StudentId, PredmetId, Value)
                                 VALUES ((SELECT StudentId FROM Students WHERE FirstName=@FirstName AND GroupName=@GroupName),
                                         (SELECT PredmetId FROM Predmet WHERE PredmetName=@PredmetName),
                                         @Value)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", studentTextBox.Text);
                command.Parameters.AddWithValue("@GroupName", groupTextBox.Text);
                command.Parameters.AddWithValue("@PredmetName", subjectTextBox.Text);
                command.Parameters.AddWithValue("@Value", gradeTextBox.Text);

                command.ExecuteNonQuery();
            }

            LoadData();
        }

        private void UpdateGradeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)gradesDataGrid.SelectedItem;

            if (row != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // запрос для изменения записи в таблице оценок
                    string query = @"UPDATE Ocenka
                                     SET Value=@Value
                                     WHERE StudentId=(SELECT StudentId FROM Students WHERE FirstName=@FirstName AND GroupName=@GroupName)
                                       AND PredmetId=(SELECT PredmetId FROM Predmet WHERE PredmetName=@PredmetName)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", row["FirstName"]);
                    command.Parameters.AddWithValue("@GroupName", row["GroupName"]);
                    command.Parameters.AddWithValue("@PredmetName", row["PredmetName"]);
                    command.Parameters.AddWithValue("@Value", gradeTextBox.Text);

                    command.ExecuteNonQuery();
                }

                LoadData();
            }
        }
    }
}
