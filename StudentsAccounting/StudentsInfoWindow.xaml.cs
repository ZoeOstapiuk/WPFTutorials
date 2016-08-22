using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StudentsAccounting
{
    /// <summary>
    /// Interaction logic for StudentsInfo.xaml
    /// </summary>
    public partial class StudentsInfoWindow : Window
    {
        private string[] availableGrades;
        private Student passedStudentValue;

        public StudentsInfoWindow(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            InitializeComponent();
            
            this.passedStudentValue = student.Clone() as Student;
            this.DataContext = passedStudentValue;

            #region Grades ComboBox Settings
            availableGrades = new string[102];
            availableGrades[0] = "N/A";
            for (int i = 1; i < availableGrades.Length; i++)
            {
                availableGrades[i] = (i - 1).ToString();
            }

            this.g1.ItemsSource = availableGrades;
            this.g2.ItemsSource = availableGrades;
            this.g3.ItemsSource = availableGrades;
            this.g4.ItemsSource = availableGrades;
            this.g5.ItemsSource = availableGrades;
            this.g6.ItemsSource = availableGrades;
            #endregion
        }

        public string[] AvailableGrades()
        {
            return availableGrades;
        }

        public Student PassedStudentValue
        {
            get
            {
                if (this.DialogResult == true)
                {
                    return passedStudentValue;
                }
                return null;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(this.fName) || Validation.GetHasError(this.lName) || Validation.GetHasError(this.birthDate))
            {
                MessageBox.Show("Please, enter only correct values or get the hell out of here!");
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
