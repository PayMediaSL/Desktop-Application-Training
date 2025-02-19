using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows;
using System.Text.RegularExpressions;
using LearningDesctopApplication.Model;


namespace LearningDesctopApplication
{
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string nic = txtNIC.Text.Trim();
            string password = txtPassword.Password;
            if (string.IsNullOrEmpty(nic) && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Fields Can not be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password Can not be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            string hashedPassword = Actions.HashPassword(password);

            ApplicationContext context = new ApplicationContext();

            DbContext? user = context.Users.FirstOrDefault(u => u.NIC == nic && u.Password == hashedPassword);

            if (user != null)
            {
                MainWindow mainForm = new MainWindow();
                mainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid NIC or Password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registerForm = new();
            registerForm.Show();
            this.Close();
        }
        private void txtNIC_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Actions.ValidateNIC(txtNIC.Text, e);
        }

        private void txtNIC_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtNIC.Text.Length == 12)
            {
                txtPassword.Focus();
            }
            else if (txtNIC.Text.EndsWith('v') || (txtNIC.Text.EndsWith('V')))   
            {
                txtPassword.Focus();
            }
        }
    }
}
