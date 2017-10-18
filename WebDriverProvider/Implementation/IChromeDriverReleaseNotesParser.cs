namespace WebDriverProvider.Implementation
{
	internal interface IChromeDriverReleaseNotesParser
	{
		string FindCompatibleDriverVersion(string releaseNotes, string requiredChromeVersion);
	}
}