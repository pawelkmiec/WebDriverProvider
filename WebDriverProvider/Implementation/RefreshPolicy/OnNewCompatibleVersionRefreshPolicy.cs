using System;
using System.Collections.Generic;
using System.IO;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	internal class OnNewCompatibleVersionRefreshPolicy : IRefreshPolicy
	{
		private readonly IShellCommandExecutor _shellCommandExecutor;

		public OnNewCompatibleVersionRefreshPolicy(IShellCommandExecutor shellCommandExecutor)
		{
			_shellCommandExecutor = shellCommandExecutor;
		}

		public bool ShouldDownload(IWebDriverInfo newDriverInfo, DirectoryInfo downloadDirectory)
		{
			_shellCommandExecutor.WorkingDirectory = downloadDirectory.FullName;
			var existingDriverVersionString = newDriverInfo.GetDriverVersion(_shellCommandExecutor);
			var existingDriverVersion = ParseVersion(existingDriverVersionString);

			var newDriverVersionString = newDriverInfo.Version;
			var newDriverVersion = ParseVersion(newDriverVersionString);

			return Version.ComparerInstance.Compare(newDriverVersion, existingDriverVersion) > 0;
		}

		private Version ParseVersion(string driverVersionString)
		{
			var parts = driverVersionString.Split('.');

			if (parts.Length == 1)
			{
				var majorNumber = Int32.Parse(parts[0]);
				return new Version(majorNumber, 0);
			}
			else
			{
				var majorNumber = Int32.Parse(parts[0]);
				var minorNumber = Int32.Parse(parts[1]);
				return new Version(majorNumber, minorNumber);
			}
		}

		private class Version
		{
			public Version(int majorNumber, int minorNumber)
			{
				MajorNumber = majorNumber;
				MinorNumber = minorNumber;
			}

			public int MajorNumber { get; }

			public int MinorNumber { get; }

			public static Comparer<Version> ComparerInstance { get; } = new VersionComparer();

			private sealed class VersionComparer : Comparer<Version>
			{
				public override int Compare(Version x, Version y)
				{
					if (ReferenceEquals(x, y)) return 0;
					if (ReferenceEquals(null, y)) return 1;
					if (ReferenceEquals(null, x)) return -1;
					var majorNumberComparison = x.MajorNumber.CompareTo(y.MajorNumber);
					if (majorNumberComparison != 0) return majorNumberComparison;
					return x.MinorNumber.CompareTo(y.MinorNumber);
				}
			}
		}
	}
}