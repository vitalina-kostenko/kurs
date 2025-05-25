using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public partial class TaskListViewModel
    {
        private void ToggleExpand(TaskItem? task)
        {
            if (task != null)
                task.IsExpanded = !task.IsExpanded;
        }

        private void Remind(TaskItem? task)
        {
            if (task is TimedTask timed)
            {
                MessageBox.Show(
                    $"Нагадування: \"{timed.Title}\"\nДедлайн: {timed.DueDate.ToString("dd.MM.yyyy")}",
                    "Нагадування",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }
    }
}
