using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDoList.Data
{
    public abstract class JsonStorageBase<T>
    {
        protected virtual string RootFolderPath => Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..", "..", "..", "JsonData"
        );

        protected abstract string RelativeFilePath { get; }

        protected string FilePath => Path.Combine(RootFolderPath, RelativeFilePath);
        
        protected readonly List<JsonConverter> Converters = new List<JsonConverter>
        {
            new JsonStringEnumConverter()
        };

        private JsonSerializerOptions? _jsonOptions;

        protected JsonSerializerOptions JsonOptions
        {
            get
            {
                if (_jsonOptions == null)
                {
                    _jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    foreach (var conv in Converters)
                        _jsonOptions.Converters.Add(conv);
                }
                return _jsonOptions;
            }
        }

        public virtual void Save(T obj)
        {
            EnsureDirectoryExists();
            var json = JsonSerializer.Serialize(obj, JsonOptions);
            File.WriteAllText(FilePath, json);
        }

        public virtual T? Load()
        {
            if (!File.Exists(FilePath))
                return default;

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public virtual void Clear()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        private void EnsureDirectoryExists()
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
