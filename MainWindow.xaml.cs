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

namespace Student
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
        private void ViewTeachers_Click(object sender, RoutedEventArgs e)
        {
            TeachersWindow teachersWindow = new TeachersWindow();
            teachersWindow.Show();
        }
        private void ViewStudents_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно для просмотра списка студентов
            StudentWindow studentWindow = new StudentWindow();

            // Показываем окно
            studentWindow.ShowDialog();
        }

        private void ViewSubjects_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно для просмотра списка предметов
            SubjectWindow subjectWindow = new SubjectWindow();

            // Показываем окно
            subjectWindow.ShowDialog();
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно для просмотра списка оценок
            GradeWindow gradeWindow = new GradeWindow();

            // Показываем окно
            gradeWindow.ShowDialog();
        }

        private void ViewAbsences_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно для просмотра списка пропусков
            AbsenceWindow absenceWindow = new AbsenceWindow();

            // Показываем окно
            absenceWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно для просмотра списка пропусков
            Analysis analysisWindow = new Analysis();

            // Показываем окно
            analysisWindow.ShowDialog();
            
        }
    }
}
