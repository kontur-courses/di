namespace TagsCloudContainer.FileOpeners
{
    /// <summary>
    ///  Нужен чтобы можно было реализовать открытие разных форматов
    /// </summary>
    public interface IFileReader
    {
        public string ReadFile(string filePath);
    }
}