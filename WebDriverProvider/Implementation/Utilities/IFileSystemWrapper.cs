using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Utilities
{
	internal interface IFileSystemWrapper
    {
	    DirectoryInfo GetCurrentDirectory();
	    Task SaveStream(Stream streamToSave, FileInfo saveFilePath);
	    Task<Stream> UnzipSingleFile(Stream zippedStream);
	    void Delete(FileInfo file);
	    bool FileExists(string fileName, DirectoryInfo directory);
    }
}
