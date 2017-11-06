using System;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class VersionSpecificUrl
	{
		public Uri Url { get; set; }

		public DriverVersion Version { get; set; }
	}
}