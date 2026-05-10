using Lab9.Green;
using System;

namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        protected GreenFileManager(string name) : base(name) { }

        protected GreenFileManager(string name, string folder, string file, string extension = "txt")
            : base(name, folder, file, extension) { }

        public abstract void Serialize<T>(T obj) where T : Green;

        public abstract T Deserialize<T>() where T : Green;

        public override void EditFile(string text)
        {
            if (string.IsNullOrWhiteSpace(FullPath))
                throw new InvalidOperationException("FullPath не задан.");
        }

        public override void ChangeFileExtension(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                throw new ArgumentException("Расширение не может быть пустым.");
        }
    }
}
