using System;
using System.IO;
using System.Linq;
using TagsCloudVisualisation.App.InputStream.FileInputStream.Exceptions;

namespace TagsCloudVisualisation.App.InputStream.FileInputStream
{
    /// <summary>
    /// Класс, который позволяет получать текст из текстовыйх файлов формата txt
    /// </summary>
    public class TxtEncoder : IFileEncoder
    {
        private const string FileType = "txt";

        /// <summary>
        /// Метод, который позволяет получить текст из файла
        /// </summary>
        /// <param name="fileName">Полный путь к файлу</param>
        /// <exception cref="IncorrectFileTypeException">Если тип файла не совпадает с ожидаемым типом</exception>
        /// <exception cref="ArgumentException">Если не получается прочитать данные из файла по каким-либо причинам</exception>
        /// <returns>Текстовое значение файла</returns>
        public string GetText(string fileName)
        {
            if (!IsCompatibleFileType(FileType))
                throw new IncorrectFileTypeException(FileType, fileName
                    .Split('.')
                    .LastOrDefault() ?? string.Empty);
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Can not read words from file", e);
            }
            
        }

        public bool IsCompatibleFileType(string fileName)
        {
            return fileName.EndsWith(FileType);
        }

        public string GetExpectedFileType()
        {
            return FileType;
        }
    }
}