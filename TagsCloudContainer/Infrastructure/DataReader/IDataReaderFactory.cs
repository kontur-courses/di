using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure.DataReader
{
    internal interface IDataReaderFactory
    {
        public IDataReader CreateDataReader(AppSettings settings);
    }
}