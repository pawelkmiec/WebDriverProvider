using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Utilities
{
	internal class FileSystemWrapper : IFileSystemWrapper
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

		public Task<Stream> UnzipSingleFile(Stream zippedStream)
		{
			using (var zip = new ZipArchive(zippedStream, ZipArchiveMode.Read))
			{
				var zipEntry = zip.Entries.Single();
				return Task.FromResult(zipEntry.Open());
			}
		}

		public void Delete(FileInfo file)
		{
			file.Delete();
		}

		public bool FileExists(string fileName, DirectoryInfo directory)
		{
			throw new NotImplementedException();
		}
	}
}