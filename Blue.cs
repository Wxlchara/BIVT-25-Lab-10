using System;
using Lab9.Blue;

namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private T[] tasks;
        private BlueFileManager<T> manager;

        public BlueFileManager<T> Manager => manager;
        public T[] Tasks => tasks;

        public Blue(T[] tasks = null)
        {
            if (tasks != null)
            {
                this.tasks = new T[tasks.Length];
                Array.Copy(tasks, this.tasks, tasks.Length);
            }
            else
            {
                this.tasks = new T[0];
            }
        }

        public Blue(BlueFileManager<T> manager, T[] tasks = null) : this(tasks)
        {
            this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public Blue(T[] tasks, BlueFileManager<T> manager) : this(manager, tasks)
        {
        }

        public void Add(T item)
        {
            if (item == null) return;
            T[] newArray = new T[tasks.Length + 1];
            for (int i = 0; i < tasks.Length; i++)
                newArray[i] = tasks[i];
            newArray[tasks.Length] = item;
            tasks = newArray;
        }

        public void Add(T[] items)
        {
            if (items == null) return;
            foreach (T item in items)
                Add(item);
        }

        public void Remove(T item)
        {
            if (item == null) return;
            int removeIndex = -1;
            for (int i = 0; i < tasks.Length; i++)
            {
                if (ReferenceEquals(tasks[i], item))
                {
                    removeIndex = i;
                    break;
                }
            }
            if (removeIndex < 0) return;

            T[] newArray = new T[tasks.Length - 1];
            int j = 0;
            for (int i = 0; i < tasks.Length; i++)
            {
                if (i != removeIndex)
                    newArray[j++] = tasks[i];
            }
            tasks = newArray;
        }

        public void Clear()
        {
            tasks = new T[0];
            if (manager != null)
            {
                string folder = manager.FolderPath;
                if (!string.IsNullOrEmpty(folder) && System.IO.Directory.Exists(folder))
                    System.IO.Directory.Delete(folder, true);
            }
        }

        public void SaveTasks()
        {
            if (manager == null)
                throw new InvalidOperationException("Manager is not set.");
            for (int i = 0; i < tasks.Length; i++)
            {
                manager.ChangeFileName($"task_{i}");
                manager.Serialize(tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (manager == null)
                throw new InvalidOperationException("Manager is not set.");
            for (int i = 0; i < tasks.Length; i++)
            {
                manager.ChangeFileName($"task_{i}");
                try
                {
                    tasks[i] = manager.Deserialize();
                }
                catch
                {
                    tasks[i] = null;
                }
            }
        }

        public void ChangeManager(BlueFileManager<T> newManager)
        {
            manager = newManager ?? throw new ArgumentNullException(nameof(newManager));
            string folderName = newManager.Name;
            if (!string.IsNullOrEmpty(folderName))
            {
                if (!System.IO.Directory.Exists(folderName))
                    System.IO.Directory.CreateDirectory(folderName);
                newManager.SelectFolder(folderName);
            }
        }
    }
}