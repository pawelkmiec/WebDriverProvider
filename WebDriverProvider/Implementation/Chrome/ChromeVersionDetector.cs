using System.Linq;
using Microsoft.Win32;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ChromeVersionDetector : IBrowserVersionDetector
    {
	    public string Detect()
	    {
		    var chromeVersionKey = Registry.CurrentUser.OpenSubKey("Software\\Google\\Chrome\\BLBeacon");
		    if (chromeVersionKey?.GetValue("version") is string version)
		    {
			    var versionParts = version.Split('.');
			    return versionParts.First();
		    }
			return null;
	    }
    }
}
