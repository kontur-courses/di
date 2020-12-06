using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    internal class DataReaderFactory : IDataReaderFactory
    {
        private readonly InputSettings settings;

        public DataReaderFactory(InputSettings settings)
        {
            this.settings = settings;
        }

        public IDataReader CreateDataReader()
        {
            return new TxtFileReader(settings.InputFileName);
        }
    }
}