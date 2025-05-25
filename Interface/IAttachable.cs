using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Interface
{
    public interface IAttachable
    {
        string? AttachmentPath { get; set; }
        void OpenAttachment();
    }
}
