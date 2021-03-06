﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Tests.Integration
{
	public class FileSystemWrapperTests
	{
		private FileSystemWrapper _fileSystem;

		[SetUp]
		public void Setup()
		{
			_fileSystem = new FileSystemWrapper();
		}

		[Test]
		public async Task should_be_able_to_read_saved_stream_contents()
		{
			//given
			var streamContentsAsString = "stream contents";
			var streamContents = Encoding.ASCII.GetBytes(streamContentsAsString);
			var savePath = new FileInfo("SavedStream.txt");

			//when
			await _fileSystem.SaveStream(new MemoryStream(streamContents), savePath);

			//then
			var fileBytes = File.ReadAllBytes(savePath.FullName);
			var fileConentsAsString = Encoding.ASCII.GetString(fileBytes);
			Assert.That(fileConentsAsString, Is.EqualTo(streamContentsAsString));
		}

		[Test]
		public async Task should_read_file_from_unzipped_archive()
		{
			//given
			var zipName = "TestArchive";
			var zippedFileName = "ZippedFile.txt";
			var zippedFileContents = "zipped file contents";
			var zipFile = CreateZipFile(zipName, zippedFileName, zippedFileContents);
			var fileStream = File.OpenRead(zipFile.FullName);

			//when
			var unzippedFile = await _fileSystem.UnzipSingleFile(fileStream);

			//then
			using (var stream = new StreamReader(unzippedFile))
			{
				var unzippedFileConents = await stream.ReadToEndAsync();
				Assert.That(unzippedFileConents, Is.EqualTo(zippedFileContents));
			}
		}

		[Test]
		public void should_return_true_when_file_is_present()
		{
			//given
			var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
			var fileName = Guid.NewGuid().ToString("N") + ".txt";
			var fullFilePath = Path.Combine(directory.FullName, fileName);
			File.Create(fullFilePath);

			//when
			var fileExists = _fileSystem.FileExists(fileName, directory);

			//then
			Assert.That(fileExists, Is.True);
		}

		private static FileInfo CreateZipFile(string zipName, string zippedFileName, string zippedFileContents)
		{
			var zipContentsDirectory = new DirectoryInfo(zipName);
			if (zipContentsDirectory.Exists)
			{
				zipContentsDirectory.Delete(true);
			}
			zipContentsDirectory.Create();

			var zippedFilePath = Path.Combine(zipContentsDirectory.FullName, zippedFileName);
			File.WriteAllText(zippedFilePath, zippedFileContents);

			var zipFile = new FileInfo($"{zipName}.zip");

			if (zipFile.Exists)
			{
				zipFile.Delete();
			}
			ZipFile.CreateFromDirectory(zipContentsDirectory.FullName, zipFile.FullName);
			return zipFile;
		}
	}
}
