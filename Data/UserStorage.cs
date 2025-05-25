using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class UserStorage : JsonStorageBase<List<User>>
    {
        protected override string RelativeFilePath => "users.json";

        public bool ValidateUser(string username, string password)
        {
            var users = Load();
            if (users == null)
            {
                users = new List<User>();
            }
            return users.Exists(u => u.Username == username && u.Password == password);
        }

        public void AddUser(User user)
        {
            var users = Load();
            if (users == null)
            {
                users = new List<User>();
            }
            users.Add(user);
            Save(users);
        }
    }
}
