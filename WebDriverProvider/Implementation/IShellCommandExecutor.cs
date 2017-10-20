namespace WebDriverProvider.Implementation
{
	internal interface IShellCommandExecutor
	{
		string WorkingDirectory { get; set; }
		string Execute(string command);
	}
}