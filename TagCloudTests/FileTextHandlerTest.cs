using System.Text;
using FluentAssertions;
using TagCloud.TextHandlers;
using TagCloudTests.TestData;

namespace TagCloudTests;

public class FileTextHandlerTest
{
    private FileTextHandler handler;

    [TestCaseSource(typeof(FileTextHandlerTestData), nameof(FileTextHandlerTestData.Data))]
    public void Test(string input, (string, int)[] output)
    {
        handler = new FileTextHandler(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        handler.Handle()
            .Should()
            .BeEquivalentTo(output);
    }
}