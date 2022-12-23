namespace TagsCloudContainer.FileReaders
{
    /// <summary>
    ///  Нужен чтобы можно было реализовать открытие разных форматов
    /// </summary>
    public interface IFileReader
    {
        public string[] FileToWordsArray(string filePath);
    }
}