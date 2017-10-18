using System.IO;
using NUnit.Framework;

namespace WebDriverProvider.Tests.Unit.RefreshPolicy
{
	public class OneNewCompatibleVersionRefreshPolicyTests
	{
		private OnNewCompatibleVersionRefreshPolicy _onNewCompatibleVersionRefreshPolicy;

		[SetUp]
		public void Setup()
		{
			_onNewCompatibleVersionRefreshPolicy = new OnNewCompatibleVersionRefreshPolicy();
		}

		[Test]
		public void should_()
		{
			//given
			// onNewCompatibleVersionRefreshPolicy.	
	
			//when

			//then
			Assert.That(true, Is.EqualTo(false));
		}
	}

	public class OnNewCompatibleVersionRefreshPolicy : IRefreshPolicy
	{
		public bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory)
		{
			throw new System.NotImplementedException();
		}
	}
}