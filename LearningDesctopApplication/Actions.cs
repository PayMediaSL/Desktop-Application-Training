using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LearningDesctopApplication
{
    public static class Actions
    {
        public static string HashPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
        public static void ValidateNIC(string nic, TextCompositionEventArgs e)
        {
            Regex regex = new(@"^\d{0,8}$|^\d{9}[Vv]?$|^\d{0,12}$");
            string nextInput = nic + e.Text;
            if (!regex.IsMatch(nextInput))
            {
                e.Handled = true;
            }
        }
        public static bool ValidatePassword(string password)
        {
            Regex regex = new(@"^(?=.*[A-Z])(?=.*\W).{8,}$");
            return regex.IsMatch(password);
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
    
}
