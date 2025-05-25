using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Interface;

namespace ToDoList.Models
{
    public class TimedTask : BasicTask, ITimedTask
    {
        public DateTime DueDate { get; set; }

        public TimedTask(string title, DateTime dueDate) : base(title)
        {
            DueDate = dueDate;
        }

        public override string BaseDescription => $"{Title} - Due: {DueDate.ToShortDateString()}";

        public string GetTimedDescription()
        {
            return $"Task: {Title}, due {DueDate.ToShortDateString()}";
        }
    }
}
