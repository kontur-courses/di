using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;

namespace TagsCloudTests
{
	[TestFixture]
	public class TxtReader_Tests
	{
		private string testResourcesDirectory;
		
		[OneTimeSetUp]
		public void OneTimeSetUp() => 
			testResourcesDirectory = TestContext.CurrentContext.TestDirectory + @"/../../Resources/Tests";

		[Test]
		public void Read_ThrowsException_WhenFileIsEmpty()
		{
			var fileName = testResourcesDirectory + "empty.txt";
			var reader = new TxtReader(fileName);

			Action action = () => reader.Read();
			action.Should().Throw<Exception>();
		}
	}
}