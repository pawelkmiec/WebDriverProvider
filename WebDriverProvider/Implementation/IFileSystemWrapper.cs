using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface IFileSystemWrapper
    {
	    DirectoryInfo GetCurrentDirectory();
	    Task SaveStream(Stream streamToSave, FileInfo saveFilePath);
	    Task<FileInfo> Unzip(FileInfo zipFile);
	    void Delete(FileInfo file);
	    bool FileExists(string fileName, DirectoryInfo directory);
    }
}
