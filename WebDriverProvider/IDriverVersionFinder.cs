using System;
using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IDriverVersionFinder
	{
		Task<Uri> FindDriverUrl();
	}
}