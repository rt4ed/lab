using System;
using System.IO;
using System.Threading;
using FileMonitoring.Interfaces;
using Moq;
using NUnit.Framework;

namespace FileMonitoring.Tests
{
	[TestFixture, NonParallelizable]
	public class FileMonitorTests
	{
		private const string BackupPath = "_backups";

		private Mock<IConfiguration> _configurationMock;
		private static readonly string FilesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Files");
		private static readonly string TempFilesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TempFiles");

		private IConfiguration Configuration => _configurationMock.Object;
		
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_configurationMock = new Mock<IConfiguration>();
			_configurationMock.Setup(c => c.BackupPath).Returns(BackupPath);
			_configurationMock.Setup(c => c.Path).Returns(TempFilesPath);

			Directory.CreateDirectory(TempFilesPath);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			Directory.Delete(TempFilesPath, true);
		}

		[SetUp]
		public void SetUp()
		{
			foreach (var file in Directory.GetFiles(FilesPath))
			{
				File.Copy(file, Path.Combine(TempFilesPath, Path.GetFileName(file)), true);
			}
		}

		[TearDown]
		public void TearDown()
		{
			foreach (var file in Directory.GetFiles(TempFilesPath))
			{
				File.Delete(file);
			}
		}

		[Test]
		public void Start_ModifyTextFile_FileContainsOriginalContentAfterReset()
		{
			using (var monitor = GetFileMonitor())
			{
				monitor.Start();

				var file1Path = Path.Combine(TempFilesPath, "NewFile1.txt");
				var initialFile1Content = File.ReadAllText(file1Path);

				var dateTimeToRestore = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLines(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);

				monitor.Reset(dateTimeToRestore);
				var restoredFile1Content = File.ReadAllText(file1Path);

				Assert.AreEqual(initialFile1Content, restoredFile1Content);
			}
		}

		[Test]
		public void Start_ModifyAnotherFile_FileIsModifiedAfterReset()
		{
			using (var monitor = GetFileMonitor())
			{
				monitor.Start();

				var logFilePath = Path.Combine(TempFilesPath, "NewFile3.log");
				var initialLogFileContent = File.ReadAllText(logFilePath);
				Thread.Sleep(1000);

				var dateTimeToRestore = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(logFilePath, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);

				monitor.Reset(dateTimeToRestore);
				var contentAfterRestore = File.ReadAllText(logFilePath);

				Assert.AreNotEqual(initialLogFileContent, contentAfterRestore);
			}
		}

		[Test]
		public void Start_ModifyTextFileSomeTimes_FileContainsExpectedContentAfterReset()
		{
			using (var monitor = GetFileMonitor())
			{
				monitor.Start();

				var file1Path = Path.Combine(TempFilesPath, "NewFile1.txt");
				var initialFile1Content = File.ReadAllText(file1Path);
				Thread.Sleep(1000);

				var dateTimeToRestore1 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);
				var file1Content1 = File.ReadAllText(file1Path);
				Thread.Sleep(1000);

				var dateTimeToRestore2 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);
				var file1Content2 = File.ReadAllText(file1Path);
				Thread.Sleep(1000);

				var dateTimeToRestore3 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);

				monitor.Reset(dateTimeToRestore1);
				var restoredFile1Content = File.ReadAllText(file1Path);
				Assert.AreEqual(initialFile1Content, restoredFile1Content);

				monitor.Reset(dateTimeToRestore3);
				restoredFile1Content = File.ReadAllText(file1Path);
				Assert.AreEqual(file1Content2, restoredFile1Content);

				monitor.Reset(dateTimeToRestore2);
				restoredFile1Content = File.ReadAllText(file1Path);
				Assert.AreEqual(file1Content1, restoredFile1Content);
			}
		}

		[Test]
		public void Start_ModifySomeTextFilesSomeTimes_FilesContainExpectedContentAfterReset()
		{
			using (var monitor = GetFileMonitor())
			{
				monitor.Start();

				var file1Path = Path.Combine(TempFilesPath, "NewFile1.txt");
				var initialFile1Content = File.ReadAllText(file1Path);
				var file2Path = Path.Combine(TempFilesPath, "NewFile2.txt");
				var initialFile2Content = File.ReadAllText(file2Path);
				Thread.Sleep(1000);

				var dateTimeToRestore1 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				File.AppendAllLinesAsync(file2Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);
				var file1Content1 = File.ReadAllText(file1Path);
				var file2Content1 = File.ReadAllText(file2Path);
				Thread.Sleep(1000);

				var dateTimeToRestore2 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);
				var file1Content2 = File.ReadAllText(file1Path);
				var file2Content2 = File.ReadAllText(file2Path);

				var dateTimeToRestore3 = DateTime.Now;
				Thread.Sleep(1000);
				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				File.AppendAllLinesAsync(file2Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);

				monitor.Reset(dateTimeToRestore1);
				var restoredFile1Content = File.ReadAllText(file1Path);
				var restoredFile2Content = File.ReadAllText(file2Path);
				Assert.AreEqual(initialFile1Content, restoredFile1Content);
				Assert.AreEqual(initialFile2Content, restoredFile2Content);

				monitor.Reset(dateTimeToRestore3);
				restoredFile1Content = File.ReadAllText(file1Path);
				restoredFile2Content = File.ReadAllText(file2Path);
				Assert.AreEqual(file1Content2, restoredFile1Content);
				Assert.AreEqual(file2Content2, restoredFile2Content);

				monitor.Reset(dateTimeToRestore2);
				restoredFile1Content = File.ReadAllText(file1Path);
				restoredFile2Content = File.ReadAllText(file2Path);
				Assert.AreEqual(file1Content1, restoredFile1Content);
				Assert.AreEqual(file2Content1, restoredFile2Content);
			}
		}

		[Test]
		public void Start_ModifyTextFile_ThereIsNotBackupDirectoryAfterDispose()
		{
			var file1Path = Path.Combine(TempFilesPath, "NewFile1.txt");
			using (var monitor = GetFileMonitor())
			{
				monitor.Start();

				File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
				Thread.Sleep(1000);
			}

			File.AppendAllLinesAsync(file1Path, new []{Guid.Empty.ToString()});
			Assert.IsFalse(Directory.Exists(Configuration.BackupPath));
		}

		[Test]
		public void Start_ModifyTextFile_ThereIsNotBackupDirectoryAfterStop()
		{
			var monitor = GetFileMonitor();
			monitor.Start();

			var file1Path = Path.Combine(TempFilesPath, "NewFile1.txt");

			File.AppendAllLinesAsync(file1Path, new[] {Guid.Empty.ToString()});
			Thread.Sleep(1000);
			monitor.Stop();

			File.AppendAllLinesAsync(file1Path, new[] {Guid.Empty.ToString()});
			Assert.IsFalse(Directory.Exists(Configuration.BackupPath));
		}

		private FileMonitor GetFileMonitor()
		{
			return new FileMonitor(Configuration);
		}
	}
}
