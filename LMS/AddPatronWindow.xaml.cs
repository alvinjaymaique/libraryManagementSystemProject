using System;
using System.Windows;
using System.Windows.Controls;
using Google.Protobuf;
using MySql.Data.MySqlClient;

namespace LMS
{
    public partial class AddPatronWindow : Window
    {
        public Patron NewPatron { get; private set; }

        public AddPatronWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text) ||
                //string.IsNullOrWhiteSpace(MembershipIDTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(AddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(DateOfBirthTextBox.Text) ||
                MembershipTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new patron
            NewPatron = new Patron
            {
                FullName = FullNameTextBox.Text,
                MembershipID = MembershipIDTextBox.Text,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Address = AddressTextBox.Text,
                DateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text),
                MembershipType = ((ComboBoxItem)MembershipTypeComboBox.SelectedItem).Content.ToString(),
                Status = "Active"
            };

            DialogResult = true;
            Close();         
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}
