﻿using System.Diagnostics;

namespace WebDriverProvider.Implementation.Utilities
{
	internal class ShellCommandExecutor : IShellCommandExecutor
	{
		public string Execute(string workingDirectory, string command, string arguments)
		{
			var processStartInfo = new ProcessStartInfo
			{
				WorkingDirectory = workingDirectory,
				FileName = command,
                Arguments = arguments,
			    RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var process = Process.Start(processStartInfo);
			var output = process.StandardOutput.ReadToEnd();
			return output;
		}
	}
}