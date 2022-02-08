using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TagsCloudContainer.Load.File;
using Xunit;

namespace TagsCloudContainer.Tests;

public class LoadTests
{
    [Fact]
    public async Task LoadFromFile()
    {
        var loader = new FileWordsLoader(new FileWordsLoaderOptions
        {
            FilePath = Path.Combine("Data", "test.txt")
        });
        var words = await loader.GetWordsAsync(CancellationToken.None);
        words.Should().NotBeEmpty().And.HaveCount(3);
    }
}