using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDriverProvider.Implementation
{
	internal class DriverVersion
	{
		public DriverVersion(int majorNumber, int minorNumber)
		{
			MajorNumber = majorNumber;
			MinorNumber = minorNumber;
		}

		public int MajorNumber { get; }

		public int MinorNumber { get; }

		public static Comparer<DriverVersion> ComparerInstance { get; } = new VersionComparer();

		private sealed class VersionComparer : Comparer<DriverVersion>
		{
			public override int Compare(DriverVersion x, DriverVersion y)
			{
				if (ReferenceEquals(x, y)) return 0;
				if (ReferenceEquals(null, y)) return 1;
				if (ReferenceEquals(null, x)) return -1;
				var majorNumberComparison = x.MajorNumber.CompareTo(y.MajorNumber);
				if (majorNumberComparison != 0) return majorNumberComparison;
				return x.MinorNumber.CompareTo(y.MinorNumber);
			}
		}

		public static DriverVersion ParseUrlVersionString(string versionString)
		{
		    return ParseMajorMinorVersionString(versionString);
		}

	    public static DriverVersion ParseCommandLineVersionString(string versionString)
	    {
            // ChromeDriver 2.33.506120 (e3e53437346286c0bc2d2dc9aa4915ba81d9023f)
	        var versionParts = versionString.Trim().Split(' ').ToArray();
	        var secondVersionPart = versionParts[1];
            var version =  ParseMajorMinorVersionString(secondVersionPart);
            return version;
	    }

	    private static DriverVersion ParseMajorMinorVersionString(string versionString)
	    {
	        var parts = versionString.Split('.');

	        if (parts.Length == 1)
	        {
	            var majorNumber = Int32.Parse(parts[0]);
	            return new DriverVersion(majorNumber, 0);
	        }
	        else
	        {
	            var majorNumber = Int32.Parse(parts[0]);
	            var minorNumber = Int32.Parse(parts[1]);
	            return new DriverVersion(majorNumber, minorNumber);
	        }
	    }
	}
}