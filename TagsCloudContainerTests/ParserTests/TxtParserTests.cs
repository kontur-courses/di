using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class TxtParserTests : ParserTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            parser = new TxtParser();
            format = "txt";
        }
    }
}
