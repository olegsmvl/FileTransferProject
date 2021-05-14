using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace App1
{
    public class FileWatcher
    {
        public delegate void FileHandler(string message);
        public event FileHandler FileEvent;
        private Settings Settings;

        public FileWatcher(Settings settings)
        {
            Settings = settings;
        }

        public void Start()
        {
            var watcher = new FileSystemWatcher(Settings.Folder);
            watcher.Created += Watcher_Event;
            watcher.Changed += Watcher_Event;
            watcher.Renamed += Watcher_Event;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Event(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"file event = {e.FullPath}");

            if (FileEvent != null)
                FileEvent(e.FullPath);
        }
    }
}
