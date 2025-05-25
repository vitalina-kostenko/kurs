using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interface;
using System.IO;

namespace ToDoList.Models
{
    public class BasicTask : TaskItem
    {
        public BasicTask(string title) : base(title) { }
    }
}