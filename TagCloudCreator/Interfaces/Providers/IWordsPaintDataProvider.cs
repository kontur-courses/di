using TagCloudCreator.Infrastructure;

namespace TagCloudCreator.Interfaces.Providers;

public interface IWordsPaintDataProvider
{
    IEnumerable<WordPaintData> GetWordsPaintData();
}