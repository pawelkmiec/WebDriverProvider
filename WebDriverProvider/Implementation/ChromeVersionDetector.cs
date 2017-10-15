using System.Linq;
using Microsoft.Win32;

namespace WebDriverProvider.Implementation
{
    public class ChromeVersionDetector : IChromeVersionDetector
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
