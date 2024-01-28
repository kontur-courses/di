using System.Text;
using TagCloud.Excluders;
using TagCloud.TextHandlers;
using TagCloud.WordFilters;
using TagCloudTests.TestData;

namespace TagCloudTests;

public class FileTextHandlerTests
{
    private FileTextHandler handler;

    [TestCaseSource(typeof(FileTextHandlerTestData), nameof(FileTextHandlerTestData.Data))]
    public void Handle(string input, Dictionary<string, int> output)
    {
        handler = new FileTextHandler(new MemoryStream(Encoding.UTF8.GetBytes(input)), new MyStemWordFilter());
        handler.Handle()
            .Should()
            .BeEquivalentTo(output);
    }
}