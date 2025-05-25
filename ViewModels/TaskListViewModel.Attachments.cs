using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interface;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public partial class TaskListViewModel
    {
        private void AttachFile(TaskItem? task)
        {
            if (task is not IAttachable attachable) return;

            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                attachable.AttachmentPath = dialog.FileName;
            }
            SaveTasks();
        }

        private void OpenAttachment(TaskItem? task)
        {
            if (task is not IAttachable attachable) return;

            attachable.OpenAttachment();
        }
    }
}
