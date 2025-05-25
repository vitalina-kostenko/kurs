using System;
using System.Linq;
using System.Windows;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in both fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var userStorage = new UserStorage(); 
            var users = userStorage.Load(); 

            if (users != null && users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("User already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            userStorage.AddUser(new User { Username = username, Password = password });

            this.DialogResult = true;
        }
    }
}
