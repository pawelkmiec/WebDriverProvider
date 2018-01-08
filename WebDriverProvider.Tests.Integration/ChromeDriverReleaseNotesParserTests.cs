using System;
using System.IO;
using NUnit.Framework;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.Chrome;

namespace WebDriverProvider.Tests.Integration
{
	public class ChromeDriverReleaseNotesParserTests
	{
		[Test]
		public void should_return_driver_2_22_when_required_chrome_version_is_49_as_per_test_file()
		{
			//given
			var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles//ChromeReleaseNotes.txt");
			var releaseNotes = File.ReadAllText(testFilePath);
			var releaseNotesParser = new ReleaseNotesParser();
			var requiredChromeVersion = "49";

			//when
			var result = releaseNotesParser.FindCompatibleDriverVersion(releaseNotes, requiredChromeVersion);

			//then
			Assert.That(result, Is.EqualTo("2.22"));
		}
	}
}
