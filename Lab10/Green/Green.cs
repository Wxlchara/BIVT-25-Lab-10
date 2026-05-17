using System;
using System.IO;

namespace Lab10.Green
{
    public class Green
    {
        private GreenFileManager _manager;
        private Lab9.Green.Green[] _tasks;

        public GreenFileManager Manager
        {
            get { return _manager; }
        }

        public Lab9.Green.Green[] Tasks
        {
            get { return (Lab9.Green.Green[])_tasks.Clone(); }
        }

        public Green(Lab9.Green.Green[] tasks = null)
        {
            _manager = null;
            _tasks = tasks == null ? new Lab9.Green.Green[0] : (Lab9.Green.Green[])tasks.Clone();
        }

        public Green(GreenFileManager manager, Lab9.Green.Green[] tasks = null)
        {
            _manager = manager;
            _tasks = tasks == null ? new Lab9.Green.Green[0] : (Lab9.Green.Green[])tasks.Clone();
        }

        public Green(Lab9.Green.Green[] tasks, GreenFileManager manager)
        {
            _manager = manager;
            _tasks = tasks == null ? new Lab9.Green.Green[0] : (Lab9.Green.Green[])tasks.Clone();
        }

        public void Add(Lab9.Green.Green task)
        {
            if (task == null)
            {
                return;
            }

            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(Lab9.Green.Green[] tasks)
        {
            if (tasks == null)
            {
                return;
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                Add(tasks[i]);
            }
        }

        public void Remove(Lab9.Green.Green task)
        {
            if (task == null || _tasks == null)
            {
                return;
            }

            int index = -1;

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == task)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return;
            }

            Lab9.Green.Green[] result = new Lab9.Green.Green[_tasks.Length - 1];

            for (int i = 0, j = 0; i < _tasks.Length; i++)
            {
                if (i != index)
                {
                    result[j] = _tasks[i];
                    j++;
                }
            }

            _tasks = result;
        }

        public void Clear()
        {
            _tasks = new Lab9.Green.Green[0];

            if (_manager != null && _manager.FolderPath != null && Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager == null || _tasks == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] != null)
                {
                    _manager.ChangeFileName("Task" + (i + 1));
                    _manager.Serialize(_tasks[i]);
                }
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || _tasks == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName("Task" + (i + 1));
                _tasks[i] = _manager.Deserialize<Lab9.Green.Green>();
            }
        }

        public void ChangeManager(GreenFileManager manager)
        {
            if (manager == null)
            {
                return;
            }

            _manager = manager;

            if (_manager.FolderPath != null && !Directory.Exists(_manager.FolderPath))
            {
                Directory.CreateDirectory(_manager.FolderPath);
            }

            _manager.SelectFolder(_manager.FolderPath);
        }
    }
}
