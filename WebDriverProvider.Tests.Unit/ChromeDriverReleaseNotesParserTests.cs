using NUnit.Framework;
using WebDriverProvider.Implementation;

namespace WebDriverProvider.Tests.Unit
{
	public class ChromeDriverReleaseNotesParserTests
	{
		private ChromeDriverReleaseNotesParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new ChromeDriverReleaseNotesParser();
		}

		[Test]
		public void should_return_matching_driver_version_when_required_chrome_version_is_in_lower_bound_of_driver_supported_range()
		{
			//given
			var releaseNotes =
				"----------ChromeDriver v2.29(2017-04-04)----------\r\n" +
				"Supports Chrome v56 - 58\r\n";

			var requiredChromeVersion = "56";

			//when
			var driverVersion = _parser.FindCompatibleDriverVersion(releaseNotes, requiredChromeVersion);

			//then
			Assert.That(driverVersion, Is.EqualTo("2.29"));
		}

		[Test]
		public void should_return_matching_driver_version_when_required_chrome_version_is_inside_driver_supported_range()
		{
			//given
			var releaseNotes =
				"----------ChromeDriver v2.29(2017-04-04)----------\r\n" +
				"Supports Chrome v56 - 58\r\n";

			var requiredChromeVersion = "57";

			//when
			var driverVersion = _parser.FindCompatibleDriverVersion(releaseNotes, requiredChromeVersion);

			//then
			Assert.That(driverVersion, Is.EqualTo("2.29"));
		}

		[Test]
		public void should_not_return_driver_version_when_required_chrome_version_is_outside_upper_bound_of_driver_supported_range()
		{
			//given
			var releaseNotes =
				"----------ChromeDriver v2.29(2017-04-04)----------\r\n" +
				"Supports Chrome v56 - 58\r\n";

			var requiredChromeVersion = "59";

			//when
			var driverVersion = _parser.FindCompatibleDriverVersion(releaseNotes, requiredChromeVersion);

			//then
			Assert.That(driverVersion, Is.Null);
		}
	}
}