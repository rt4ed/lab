using System;
using FileMonitoring.Interfaces;
using System.IO;

namespace FileMonitoring
{
	public class FileMonitor : IFileMonitor
	{
        readonly IConfiguration conf;
		readonly FileSystemWatcher watcher = new FileSystemWatcher();
		DirectoryInfo firstPath;
		DirectoryInfo lastPath;
		int schet = 0;
		public FileMonitor(IConfiguration configuration)
		{
			conf = configuration;

		}

		public void Start()
        {
            Directory.CreateDirectory(conf.BackupPath);
            firstPath = new DirectoryInfo(conf.Path);
            lastPath = new DirectoryInfo(conf.BackupPath);

			Directory.CreateDirectory(Path.Combine(conf.BackupPath, schet.ToString()));
			CopyFiles(Path.Combine(conf.BackupPath, schet.ToString()));

            watcher.Path = conf.Path;
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Filter = "*.txt";
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void CopyFiles(string pathName)
        {
            
				foreach (var item in firstPath.GetFiles())
				{
					if (item.Extension == ".txt")
						item.CopyTo(Path.Combine(pathName, item.Name));
				}
        }

        private void OnChanged(object source, FileSystemEventArgs e)
		{
			schet++;
			var path= Path.Combine(conf.BackupPath, schet.ToString());
			Directory.CreateDirectory(path);
			CopyFiles(path);
		}


		public void Stop()
		{
			Close();
		}

		public void Reset(DateTime dateTime)
		{
			watcher.Changed -= OnChanged;
			watcher.EnableRaisingEvents = false;
			
			var minTime = new DateTime(2008, 3, 1, 7, 0, 0);
			foreach (var item in lastPath.GetDirectories())
			{
				if (item.LastWriteTime <= dateTime && item.LastWriteTime > minTime)
					minTime = item.LastWriteTime;
			}

			DirectoryInfo tempInfo = new DirectoryInfo(Path.Combine(conf.BackupPath, schet.ToString()));
			foreach (var item in lastPath.GetDirectories())
			{
				if (item.LastWriteTime == minTime)
					tempInfo = item;
			}

            foreach (var item in tempInfo.GetFiles())
			{
				item.CopyTo(Path.Combine(firstPath.Name, item.Name), true);
			}

		}

		public void Dispose()
        {
            Close();
        }

        private void Close()
        {
            watcher.Changed -= OnChanged;
            watcher.Dispose();
            Directory.Delete(conf.BackupPath, true);
        }
    }
}
