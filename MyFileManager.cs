using System;
using System.IO;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string name;
        private string folderPath;
        private string fileName;
        private string fileExtension;

        public string Name => name;
        public string FolderPath => folderPath;
        public string FileName => fileName;
        public string FileExtension => fileExtension;
        public string FullPath
        {
            get
            {
                string dir = folderPath;
                string file = fileName;
                if (!string.IsNullOrEmpty(fileExtension))
                    file += "." + fileExtension;
                return string.IsNullOrEmpty(dir) ? file : Path.Combine(dir, file);
            }
        }

        public MyFileManager(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            folderPath = "";
            fileName = "default";
            fileExtension = "txt";
        }

        public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            folderPath = folderName ?? "";
            this.fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            if (fileExtension == null) fileExtension = "txt";
            if (fileExtension.StartsWith("."))
                fileExtension = fileExtension.Substring(1);
            this.fileExtension = fileExtension;
        }

        public void SelectFolder(string folderPath)
        {
            this.folderPath = folderPath ?? "";
        }

        public void ChangeFileName(string newFileName)
        {
            fileName = newFileName ?? throw new ArgumentNullException(nameof(newFileName));
        }

        public void ChangeFileFormat(string newExtension)
        {
            if (newExtension == null)
                throw new ArgumentNullException(nameof(newExtension));
            if (newExtension.StartsWith("."))
                newExtension = newExtension.Substring(1);
            fileExtension = newExtension;

            if (!File.Exists(FullPath))
                CreateFile();
        }

        public virtual void CreateFile()
        {
            string full = FullPath;
            string dir = Path.GetDirectoryName(full);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!File.Exists(full))
                File.Create(full).Close();
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string content)
        {
            CreateFile();
            File.WriteAllText(FullPath, content);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension))
                throw new ArgumentNullException(nameof(newExtension));
            string oldFullPath = FullPath;
            ChangeFileFormat(newExtension);
            string newFullPath = FullPath;

            if (File.Exists(oldFullPath))
            {
                string content = File.ReadAllText(oldFullPath);
                File.Delete(oldFullPath);
                CreateFile();
                File.WriteAllText(newFullPath, content);
            }
        }
    }
}