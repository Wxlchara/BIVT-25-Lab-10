using Lab9.Blue;
using Lab10;

namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    {
        public BlueFileManager(string name) : base(name)
        {
        }

        public BlueFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
            : base(name, folderName, fileName, fileExtension)
        {
        }

        public override void EditFile(string content)
        {
            if (string.IsNullOrEmpty(FullPath))
                throw new InvalidOperationException("FullPath is empty.");
            base.EditFile(content);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(FullPath))
                throw new InvalidOperationException("FullPath is empty.");
            base.ChangeFileExtension(newExtension);
        }

        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}