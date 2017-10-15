namespace WebDriverProvider
{
	public interface IChromeDriverReleaseNotesParser
	{
		string FindCompatibleDriverVersion(string releaseNotes, string requiredChromeVersion);
	}
}