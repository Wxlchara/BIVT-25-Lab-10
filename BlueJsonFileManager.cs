using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Lab9.Blue;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueJsonFileManager(string name) : base(name)
        {
            ChangeFileFormat("json");
        }

        public BlueJsonFileManager(string name, string folderName, string fileName, string fileExtension = "json")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("json");
        }

        public override void EditFile(string content)
        {
            T obj = Deserialize();
            if (obj == null)
                throw new InvalidOperationException("Cannot deserialize object for editing.");
            obj.ChangeText(content);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (newExtension != "json")
                return;
            base.ChangeFileExtension(newExtension);
        }

        public override void Serialize(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            string fullPath = FullPath;
            string dir = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var data = new Dictionary<string, object>
            {
                ["Type"] = obj.GetType().FullName,
                ["Input"] = obj.Input
            };

            if (obj is Lab9.Blue.Task2 task2)
            {
                FieldInfo seqField = typeof(Lab9.Blue.Task2).GetField("sequence",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (seqField != null)
                    data["Sequence"] = seqField.GetValue(task2);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(fullPath, json);
        }

        public override T Deserialize()
        {
            string fullPath = FullPath;
            if (!File.Exists(fullPath))
                return null;

            string json = File.ReadAllText(fullPath);
            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            if (!root.TryGetProperty("Type", out JsonElement typeElem)) return null;
            string typeName = typeElem.GetString();
            if (typeName == null) return null;

            Type type = Type.GetType(typeName);
            if (type == null)
            {
                try { type = Assembly.Load("Lab9").GetType(typeName); } catch { }
            }
            if (type == null) return null;

            if (!root.TryGetProperty("Input", out JsonElement inputElem)) return null;
            string input = inputElem.GetString();
            if (input == null) return null;

            List<object> args = new List<object> { input };

            if (type == typeof(Lab9.Blue.Task2))
            {
                if (root.TryGetProperty("Sequence", out JsonElement seqElem) && seqElem.ValueKind == JsonValueKind.String)
                    args.Add(seqElem.GetString());
            }

            try
            {
                object obj = Activator.CreateInstance(type, args.ToArray());
                if (obj is T tObj)
                {
                    tObj.Review();
                    return tObj;
                }
            }
            catch { }
            return null;
        }
    }
}