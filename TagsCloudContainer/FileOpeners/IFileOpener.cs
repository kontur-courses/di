namespace TagsCloudContainer.FileOpeners
{
    /// <summary>
    ///  Нужен чтобы можно было реализовать открытие разных форматов
    /// </summary>
    public interface IFileOpener
    {
        public string OpenFile();
    }
}