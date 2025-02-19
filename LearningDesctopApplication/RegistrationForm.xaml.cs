﻿using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using LearningDesctopApplication.Model;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using System.Windows.Media;

namespace LearningDesctopApplication
{
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string nic = txtNIC.Text.Trim();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;
            string confirmPassword = txtCPassword.Password;

            if (string.IsNullOrEmpty(nic) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Fields Can not be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name cannot be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Actions.IsValidEmail(email))
            {
                
                txtEmail.BorderBrush = Brushes.Red; 
                txtEmail.BorderThickness = new Thickness(2);
                txtEmail.Focus(); 
                return; 
            }

            if (!Actions.ValidatePassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain a capital letter, and a special symbol.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Confirm Password do not match.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            using (ApplicationContext context = new ())
            {
                try
                {
                    if (context.Users.Any(u => u.Email == email || u.NIC == nic))
                    {
                        MessageBox.Show("User with this NIC or Email already exists.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

            
                    var user = new Model.DbContext
                    {
                        NIC = nic,
                        Name = name,
                        Email = email,
                        Password = Actions.HashPassword(password)
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while registering the user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
      
               

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
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
                txtName.Focus();
            }
            else if (txtNIC.Text.EndsWith('v') || (txtNIC.Text.EndsWith('V')))
            {
                txtName.Focus();
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //TextBox textBox = sender as TextBox;
            

            if (Actions.IsValidEmail(txtEmail.Text))
            {
                txtEmail.BorderBrush = Brushes.Gray; // Normal border when valid
                txtEmail.BorderThickness = new System.Windows.Thickness(1);
            }
            else
            {
                txtEmail.BorderBrush = Brushes.Red; // Red border when invalid
                txtEmail.BorderThickness = new System.Windows.Thickness(2);
            }
        }

        private void txtEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            
        }
    }
}
