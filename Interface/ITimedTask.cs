using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Interface
{
    public interface ITimedTask
    {
        DateTime DueDate { get; set; }
        string GetTimedDescription();
    }
}