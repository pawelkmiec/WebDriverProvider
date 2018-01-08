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
			Stream singleFileZipStream = new SingleFileZipStream(zippedStream);
			return Task.FromResult(singleFileZipStream);
		}

		public void Delete(FileInfo file)
		{
			file.Delete();
		}

		public bool FileExists(string fileName, DirectoryInfo directory)
		{
			var fullPath = Path.Combine(directory.FullName, fileName);
			return File.Exists(fullPath);
		}

		private class SingleFileZipStream : Stream
		{
			private readonly ZipArchive _archive;

			private readonly Stream _entryStream;

			public SingleFileZipStream(Stream baseStream)
			{
				_archive = new ZipArchive(baseStream, ZipArchiveMode.Read);
				_entryStream = _archive.Entries.Single().Open();
			}

			public override void Flush()
			{
				_entryStream.Flush();
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				return _entryStream.Read(buffer, offset, count);
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				return _entryStream.Seek(offset, origin);
			}

			public override void SetLength(long value)
			{
				_entryStream.SetLength(value);
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				_entryStream.Write(buffer, offset, count);
			}

			public override bool CanRead => _entryStream.CanRead;

			public override bool CanSeek => _entryStream.CanSeek;

			public override bool CanWrite => _entryStream.CanWrite;

			public override long Length => _entryStream.Length;

			public override long Position
			{
				get => _entryStream.Position;
				set => _entryStream.Position = value;
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					_entryStream?.Dispose();
					_archive?.Dispose();
				}
				base.Dispose(disposing);
			}
		}
	}
}