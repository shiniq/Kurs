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
    /// Логика взаимодействия для AbsenceWindow.xaml
    /// </summary>
    public partial class AbsenceWindow : Window
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=School;Integrated Security=True";

        public AbsenceWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Propuski.Propuskid, Students.FirstName, Students.LastName, Predmet.PredmetName AS Predmet, Propuski.Date, Propuski.Reason " +
                    "FROM Propuski " +
                    "JOIN Students ON Propuski.Studentid = Students.Studentid " +
                    "JOIN Predmet ON Propuski.Predmetid = Predmet.Predmetid";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                absencesDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void AddAbsenceButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string subject = subjectTextBox.Text;
            DateTime date = datePicker.SelectedDate.Value;
            string reason = reasonTextBox.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=School;Integrated Security=True"))
            {
                connection.Open();

                // получение ID студента по имени и фамилии
                int studentId;
                string selectStudentIdQuery = $"SELECT Studentid FROM Students WHERE FirstName='{firstName}' AND LastName='{lastName}'";
                SqlCommand selectStudentIdCommand = new SqlCommand(selectStudentIdQuery, connection);
                studentId = (int)selectStudentIdCommand.ExecuteScalar();

                // получение ID предмета по названию
                int predmetId;
                string selectPredmetIdQuery = $"SELECT Predmetid FROM Predmet WHERE PredmetName='{subject}'";
                SqlCommand selectPredmetIdCommand = new SqlCommand(selectPredmetIdQuery, connection);
                predmetId = (int)selectPredmetIdCommand.ExecuteScalar();

                // добавление записи в таблицу Propuski
                string insertQuery = $"INSERT INTO Propuski (Studentid, Predmetid, Date, Reason) VALUES ({studentId}, {predmetId}, '{date}', '{reason}')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            LoadData();
        }
    }
}
    

