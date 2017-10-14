using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class FileSystemWrapper : IFileSystemWrapper
	{
		public DirectoryInfo GetCurrentDirectory()
		{
			var currentDirectoryPath = Directory.GetCurrentDirectory();
			return new DirectoryInfo(currentDirectoryPath);
		}

		public async Task SaveStream(Stream streamToSave, FileInfo saveFilePath)
		{
			using (var fileStream = new FileStream(saveFilePath.FullName, FileMode.Create))
			{
				await streamToSave.CopyToAsync(fileStream);
			}
		}

		public Task<FileInfo> Unzip(FileInfo zipFile)
		{
			var zipFileDirectory = Path.GetDirectoryName(zipFile.FullName);
			var tempDirectory = CreateTempDirectory(zipFileDirectory);

			try
			{
				var unzippedTempFile = UnzipSingleFileArchive(zipFile, tempDirectory);
				var finalFilePath = Path.Combine(zipFileDirectory, Path.GetFileName(unzippedTempFile));
				DeleteIfExists(finalFilePath);
				File.Move(unzippedTempFile, finalFilePath);
				return Task.FromResult(new FileInfo(finalFilePath));
			}
			finally
			{
				tempDirectory.Delete(true);
			}
		}

		private static string UnzipSingleFileArchive(FileInfo zipFile, DirectoryInfo targetDirectory)
		{
			ZipFile.ExtractToDirectory(zipFile.FullName, targetDirectory.FullName);
			var unzippedFile = targetDirectory.GetFiles().Single().FullName;
			return unzippedFile;
		}

		private static void DeleteIfExists(string targetFilePath)
		{
			if (File.Exists(targetFilePath))
			{
				File.Delete(targetFilePath);
			}
		}

		private static DirectoryInfo CreateTempDirectory(string baseDirectory)
		{
			var tempDirectoryName = Guid.NewGuid().ToString("N");
			var tempDirectoryPath = Path.Combine(baseDirectory, tempDirectoryName);
			var tempDirectory = new DirectoryInfo(tempDirectoryPath);
			tempDirectory.Create();
			return tempDirectory;
		}

		public void Delete(FileInfo file)
		{
			file.Delete();
		}
	}
}