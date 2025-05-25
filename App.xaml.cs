using System.Windows;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var sessionStorage = new SessionStorage();
            var savedUser = sessionStorage.Load();

            if (savedUser != null)
            {
                var mainWindow = new MainWindow(savedUser);
                this.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                ShowLoginFlow();
            }
        }

        private void ShowLoginFlow()
        {
            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                var user = loginWindow.LoggedInUser;
                var sessionStorage = new SessionStorage();
                sessionStorage.Save(user);

                var mainWindow = new MainWindow(user);
                this.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
