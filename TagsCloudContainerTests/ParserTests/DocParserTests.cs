using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class DocParserTests : ParserTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            parser = new DocParser();
            format = "docx";
        }
    }
}