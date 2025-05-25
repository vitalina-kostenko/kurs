using ToDoList.Models;
using System.IO;

namespace ToDoList.Data
{
    public class SessionStorage : JsonStorageBase<User>
    {
        protected override string RelativeFilePath => Path.Combine("session", "session.json");
    }
}
