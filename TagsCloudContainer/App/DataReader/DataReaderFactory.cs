using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    internal class DataReaderFactory : IDataReaderFactory
    {
        public IDataReader CreateDataReader(AppSettings settings)
        {
            return new TxtFileReader(settings.InputFileName);
        }
    }
}