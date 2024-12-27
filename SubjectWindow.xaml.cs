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
    /// Логика взаимодействия для SubjectWindow.xaml
    /// </summary>
    public partial class SubjectWindow : Window
    {
        public SubjectWindow()
        {
            InitializeComponent();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string subjectName = searchSubjectName.Text.Trim();
            string teacherName = searchTeacherName.Text.Trim();

            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=School;Integrated Security=True"))
            {
                connection.Open();

                string query = "SELECT Predmet.PredmetName, Prepod.FirstName, Prepod.LastName FROM Predmet JOIN Prepod ON Predmet.PredmetId = Prepod.PredmetId";

                if (!string.IsNullOrEmpty(subjectName) || !string.IsNullOrEmpty(teacherName))
                {
                    query += " WHERE ";

                    if (!string.IsNullOrEmpty(subjectName))
                    {
                        query += "Predmet.PredmetName LIKE @subjectName";

                        if (!string.IsNullOrEmpty(teacherName))
                        {
                            query += " AND ";
                        }
                    }

                    if (!string.IsNullOrEmpty(teacherName))
                    {
                        query += "Prepod.FirstName + ' ' + Prepod.LastName LIKE @teacherName";
                    }
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(subjectName))
                    {
                        command.Parameters.AddWithValue("@subjectName", "%" + subjectName + "%");
                    }

                    if (!string.IsNullOrEmpty(teacherName))
                    {
                        command.Parameters.AddWithValue("@teacherName", "%" + teacherName + "%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    subjectsGrid.ItemsSource = table.DefaultView;
                }
            }
        }
    }
}