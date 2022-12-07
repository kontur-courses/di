namespace TagsCloudVisualisation.App.InputStream.FileInputStream
{
    /// <summary>
    /// Интерфейс, котоый позволяет раскодировать файл с текстом
    /// </summary>
    public interface IFileEncoder
    {
        /// <summary>
        /// Метод, который позволяет получить текст из файла
        /// </summary>
        /// <param name="fileName">Полный путь к файлу</param>
        /// <returns>Текстовое значение файла</returns>
        string GetText(string fileName);
        
        /// <summary>
        /// Метод, который позволяет проверить, подходит ли расшифравщик для расшифравки данного файла
        /// </summary>
        /// <param name="fileName">Полный путь к файлу для расшифровки</param>
        /// <returns>true - если подходит. false - в противном случае</returns>
        bool IsCompatibleFileType(string fileName);
        
        /// <summary>
        /// Метод, который позволяет получить название типа файла, для которого подходит расшифровщик
        /// </summary>
        /// <returns>Тип ожидаемого файла</returns>
        string GetExpectedFileType();
    }
}