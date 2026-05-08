using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Lab9.Blue;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public BlueTxtFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("txt");
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
            if (newExtension != "txt")
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

            using StreamWriter writer = new StreamWriter(fullPath);
            writer.WriteLine($"Type: {obj.GetType().FullName}");
            writer.WriteLine($"Input: {obj.Input}");

            if (obj is Lab9.Blue.Task2 task2)
            {
                FieldInfo seqField = typeof(Lab9.Blue.Task2).GetField("sequence",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (seqField != null)
                {
                    object seqVal = seqField.GetValue(task2);
                    if (seqVal != null)
                        writer.WriteLine($"Sequence: {seqVal}");
                }
            }
        }

        public override T Deserialize()
        {
            string fullPath = FullPath;
            if (!File.Exists(fullPath))
                return null;

            string[] lines = File.ReadAllLines(fullPath);
            string typeName = null;
            string input = null;
            string sequence = null;

            foreach (string line in lines)
            {
                if (line.StartsWith("Type:")) typeName = line.Substring(5).Trim();
                else if (line.StartsWith("Input:")) input = line.Substring(6).Trim();
                else if (line.StartsWith("Sequence:")) sequence = line.Substring(9).Trim();
            }

            if (typeName == null || input == null) return null;

            Type type = Type.GetType(typeName);
            if (type == null)
            {
                try { type = Assembly.Load("Lab9").GetType(typeName); } catch { }
            }
            if (type == null) return null;

            List<object> args = new List<object> { input };
            if (type == typeof(Lab9.Blue.Task2) && sequence != null)
                args.Add(sequence);

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