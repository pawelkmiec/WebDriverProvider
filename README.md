# WebDriverProvider

Aim of this library is to download *proper* version of web driver automatically, to help with Selenium-based testing.

Sample usage below

```
[Test]
public async Task should_get_latest_chrome_driver_and_run_selenium_go_to_url()
{
	//given
	var driverProvider = ProviderConfiguration.For(Browser.Chrome)
		.WithDesiredDriver(DesiredDriver.Latest)
		.BuildDriverProvider();
		    
	//when
	await driverProvider.DownloadDriverBinary(_driverDirectory);
			
	//then
	Assert.DoesNotThrow(() =>
	{
		using (var driver = new ChromeDriver(_driverDirectory))
		{
			driver.Navigate().GoToUrl("google.com");
		}
	});
}
```

TODOs:

1. ~~Download latest version of chromedriver.exe~~ - done
2. Detect version of Chrome that is on machine and download latest compatible version of chromedriver.exe
3. Setup some continious integration
4. Publish on nuget
5. Implementation for Firefox
6. Implementation for Edge 


