using System;
using System.Windows;

namespace School
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        #region Predefined code

        public StudentForm()
        {
            InitializeComponent();
        }

        #endregion

        // If the user clicks OK to save the Student details, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Exercise 2: Task 2a: Check that the user has provided a first name
            if (String.IsNullOrEmpty(this.firstName.Text))
            {
               MessageBox.Show("FirstName is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // TODO: Exercise 2: Task 2b: Check that the user has provided a last name
            if (String.IsNullOrEmpty(this.lastName.Text))
            {
                MessageBox.Show("LastName is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime result;
            // TODO: Exercise 2: Task 3a: Check that the user has entered a valid date for the date of birth
            if (!DateTime.TryParse(this.dateOfBirth.Text, out result))
            {
                MessageBox.Show("DateTime is uncorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // TODO: Exercise 2: Task 3b: Verify that the student is at least 5 years old
            DateTime studentDateOfBirth = result;
            TimeSpan difference = DateTime.Now.Subtract(studentDateOfBirth);
            int ageInYears = (int)(difference.Days / 365.25);
            if (ageInYears < 5)
            {
                MessageBox.Show("DateTime can't be lower than 5 years", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
