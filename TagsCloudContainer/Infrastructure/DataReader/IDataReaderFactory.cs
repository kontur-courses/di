namespace TagsCloudContainer.Infrastructure.DataReader
{
    internal interface IDataReaderFactory
    {
        public IDataReader CreateDataReader();
    }
}