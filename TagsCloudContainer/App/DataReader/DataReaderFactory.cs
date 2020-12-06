using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    internal class DataReaderFactory : IDataReaderFactory
    {
        private readonly AppSettings settings;

        public DataReaderFactory(AppSettings settings)
        {
            this.settings = settings;
        }

        public IDataReader CreateDataReader()
        {
            return new TxtFileReader(settings.InputFileName);
        }
    }
}