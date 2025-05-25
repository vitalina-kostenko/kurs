//using System.Collections.ObjectModel;
//using ToDoList.Data;
//using ToDoList.Models;

//namespace ToDoList.ViewModels
//{
//    public class TaskListViewModel
//    {
//        private TaskStorage? _taskStorage;

//        public ObservableCollection<TaskItem> Tasks { get; } = new();

//        public void LoadTasksForUser(string username)
//        {
//            _taskStorage = new TaskStorage(username);
//            var loaded = _taskStorage.Load();
//            Tasks.Clear();
//            if (loaded != null)
//            {
//                foreach (var task in loaded)
//                    Tasks.Add(task);
//            }
//        }

//        public void SaveTasks()
//        {
//            _taskStorage?.Save(Tasks.ToList());
//        }
//    }
//}
