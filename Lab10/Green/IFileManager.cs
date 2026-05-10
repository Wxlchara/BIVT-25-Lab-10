using System;

namespace Lab10.Green
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string folder);
        void ChangeFileName(string name);
        void ChangeFileFormat(string extension);
    }
}
