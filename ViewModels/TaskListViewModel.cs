using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.Data;
using ToDoList.Interface;
using ToDoList.Models;
using static ToDoList.App;

namespace ToDoList.ViewModels
{
    public partial class TaskListViewModel : INotifyPropertyChanged
    {
        private TaskStorage _taskStorage; 

        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();
        public ICollectionView TasksView { get; }

        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
        {
            "General", "Work", "Personal", "Urgent"
        };

        public string NewTaskTitle { get; set; } = string.Empty;
        public DateTime NewTaskDate { get; set; } = DateTime.Today;
        public TimeSpan? NewTaskTime { get; set; } = null;

        public string NewTaskCategory { get; set; } = "General";

        public ICommand AddBasicCommand { get; }
        public ICommand AddTimedCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand CompleteCommand { get; }
        public ICommand AttachFileCommand { get; }
        public ICommand OpenAttachmentCommand { get; }
        public ICommand ToggleExpandCommand { get; }
        public ICommand RemindCommand { get; }
        public ICommand LogoutCommand { get; }

        private User _currentUser;

        public string Username
        {
            get
            {
                if (_currentUser != null && !string.IsNullOrEmpty(_currentUser.Username))
                    return _currentUser.Username;
                else
                    return "Guest";
            }
        }

        public TaskListViewModel(User user)
        {
            _currentUser = user;
            try
            {
                LoadTasks(_currentUser.Username);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}");
            }

            Tasks.CollectionChanged += TasksCollectionChanged;

            TasksView = (ListCollectionView)CollectionViewSource.GetDefaultView(Tasks);
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.IsCompleted), ListSortDirection.Ascending));
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.CreatedTime), ListSortDirection.Descending));

            AddBasicCommand = new Command(_ => AddBasic());
            AddTimedCommand = new Command(_ => AddTimed());
            RemoveCommand = new Command(id => Remove((Guid)id));
            CompleteCommand = new Command(id => ToggleComplete((Guid)id));
            AttachFileCommand = new Command(task => AttachFile((TaskItem)task));
            OpenAttachmentCommand = new Command(task => OpenAttachment((TaskItem)task));
            ToggleExpandCommand = new Command(task => ToggleExpand((TaskItem)task));
            RemindCommand = new Command(task => Remind((TaskItem)task));
            LogoutCommand = new Command(_ => Logout());
        }

        private void LoadTasks(string username)
        {
            _taskStorage = new TaskStorage(username);
            var loaded = _taskStorage.Load();
            Tasks.Clear();
            if (loaded != null)
            {
                foreach (var task in loaded)
                    Tasks.Add(task);
            }
        }
        public void SaveTasks()
        {
            _taskStorage?.Save(Tasks.ToList());
        }

        public event Action LogoutRequested;

        private void Logout()
        {
            LogoutRequested?.Invoke();
        }
    }
}