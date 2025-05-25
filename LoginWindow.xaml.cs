using System.Windows;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public User LoggedInUser { get; private set; }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            var userStorage = new UserStorage();

            if (userStorage.ValidateUser(username, password)) 
            {
                LoggedInUser = new User { Username = username, Password = password };
                try
                {
                    this.DialogResult = true;
                }
                catch (InvalidOperationException)
                {
                    // Window was not shown as dialog, fallback to Close
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow { Owner = this };
            bool? regResult = registerWindow.ShowDialog();

            if (regResult == true)
                MessageBox.Show("Registration successful! You can now log in.", "Registered", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
