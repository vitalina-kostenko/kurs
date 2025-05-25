using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.Data;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private User _loggedInUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            this.Title = $"ToDo List – {_loggedInUser.Username}";

            try
            {
                DataContext = new TaskListViewModel(_loggedInUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ViewModel error: {ex.Message}");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DataContext is TaskListViewModel vm)
            {
                vm.SaveTasks();
            }
            base.OnClosing(e);
        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TaskItem task)
            {
                task.IsExpanded = !task.IsExpanded;
            }
        }

        private void DetailsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is TaskItem task)
            {
                task.IsExpanded = false;
            }
        }

        private void DetailsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                AdjustTextBoxHeight(textBox);
            }
        }

        private void DetailsTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                AdjustTextBoxHeight(textBox);
            }
        }

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            textBox.Height = Double.NaN;
            textBox.Measure(new Size(textBox.ActualWidth, Double.PositiveInfinity));
            textBox.Height = textBox.DesiredSize.Height;
        }

        private void TaskTitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleBox.Text))
                PlaceholderText.Visibility = Visibility.Visible;
            else
                PlaceholderText.Visibility = Visibility.Collapsed;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var sessionStorage = new SessionStorage();
            sessionStorage.Clear();

            this.Hide();

            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true && loginWindow.LoggedInUser != null)
            {
                sessionStorage.Save(loginWindow.LoggedInUser);

                _loggedInUser = loginWindow.LoggedInUser;
                this.Title = $"ToDo List – {_loggedInUser.Username}";
                DataContext = new TaskListViewModel(_loggedInUser);

                this.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
