using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public partial class TaskListViewModel
    {
        private void AddBasic()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                var task = new BasicTask(NewTaskTitle)
                {
                    Category = NewTaskCategory
                };
                Tasks.Insert(0, task);

                NewTaskTitle = string.Empty;
                OnPropertyChanged(nameof(NewTaskTitle));

                SaveTasks();
            }
        }

        private void AddTimed()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                DateTime dueDate = NewTaskDate;

                if (NewTaskTime.HasValue)
                    dueDate = NewTaskDate.Date + NewTaskTime.Value;

                var task = new TimedTask(NewTaskTitle, dueDate)
                {
                    Category = NewTaskCategory
                };
                Tasks.Insert(0, task);

                NewTaskTitle = string.Empty;
                OnPropertyChanged(nameof(NewTaskTitle));

                SaveTasks();
            }
        }

        private void Remove(object? id)
        {
            if (id is Guid guid)
            {
                var task = Tasks.FirstOrDefault(x => x.Id == guid);
                if (task != null)
                    Tasks.Remove(task);
            }
            SaveTasks();
        }

        private void ToggleComplete(object? id)
        {
            if (id is Guid guid)
            {
                var task = Tasks.FirstOrDefault(x => x.Id == guid);
                if (task != null)
                    task.IsCompleted = !task.IsCompleted;
            }
            TasksView.Refresh();
            SaveTasks();
        }
    }
}
