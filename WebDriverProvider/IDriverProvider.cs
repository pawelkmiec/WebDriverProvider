using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IDriverProvider
	{
		Task<FileInfo> GetDriverBinary();
	}
}