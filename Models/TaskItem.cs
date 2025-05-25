using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using ToDoList.Interface;

namespace ToDoList.Models
{
    public abstract class TaskItem : INotifyPropertyChanged, IAttachable
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string TaskType => GetType().Name;
        public DateTime CreatedTime { get; } = DateTime.Now;

        private string _title;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); OnPropertyChanged(nameof(Description)); }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }


        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set { if (_isExpanded != value) { _isExpanded = value; OnPropertyChanged(); } }
        }

        private string _details = "";
        public string Details
        {
            get => _details;
            set { _details = value; OnPropertyChanged(); }
        }

        private string _category = "General";
        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        private string? _attachmentPath;
        public string? AttachmentPath
        {
            get => _attachmentPath;
            set
            {
                _attachmentPath = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Description));
            }
        }

        public virtual string BaseDescription => Title;

        public virtual string Description =>
            string.IsNullOrWhiteSpace(AttachmentPath)
                ? BaseDescription
                : $"{BaseDescription} 📎";

        public void OpenAttachment()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(AttachmentPath) && File.Exists(AttachmentPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = AttachmentPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Файл не знайдено або шлях порожній.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відкриття файлу: {ex.Message}");
            }
        }

        protected TaskItem(string title) => _title = title;

        public virtual void MarkComplete() => IsCompleted = true;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}