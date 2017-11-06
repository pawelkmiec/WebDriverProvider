namespace WebDriverProvider.Implementation.Utilities
{
	internal interface IShellCommandExecutor
	{
		string Execute(string workingDirectory, string command);
	}
}