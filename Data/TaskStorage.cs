using System.Text.Json.Serialization;
using ToDoList.Models;
using ToDoList.Converters;

namespace ToDoList.Data
{
    public class TaskStorage : JsonStorageBase<List<TaskItem>>
    {
        private readonly string _username;

        protected override string RelativeFilePath => $"users/tasks_{_username}.json";

        public TaskStorage(string username)
        {
            _username = username;
            Converters.Add(new JsonTaskItemConverter());
        }
    }
}
