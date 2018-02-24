namespace WebDriverProvider.Implementation.Chrome
{
	internal interface IReleaseNotesParser
	{
		string FindCompatibleDriverVersion(string releaseNotes, string requiredChromeVersion);
	}
}