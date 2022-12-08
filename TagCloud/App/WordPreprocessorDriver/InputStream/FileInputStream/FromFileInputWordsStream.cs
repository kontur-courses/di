using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream.Exceptions;

namespace TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream
{
    /// <summary>
    /// Класс, предоставляющий возможность организовать поток слов из файла
    /// </summary>
    public class FromFileInputWordsStream : IInputWordsStream
    {
        private string? fileName;
        private readonly IFileEncoder fileEncoder;
        private readonly Func<string, IEnumerable<string>> splitter;
        
        private List<string>? words;
        private int currentItem = -1;

        /// <param name="fileEncoder">Обработчик, который умеет получать текст из файла</param>
        /// <param name="splitter">Функция, которая умеет разделять текст на отдельные слова</param>
        public FromFileInputWordsStream(IFileEncoder fileEncoder, Func<string, IEnumerable<string>> splitter)
        {
            this.fileEncoder = fileEncoder;
            this.splitter = splitter;
        }
        
        public FromFileInputWordsStream(string filename, IFileEncoder fileEncoder, Func<string, IEnumerable<string>> splitter)
        {
            this.fileEncoder = fileEncoder;
            this.splitter = splitter;
            SelectFile(filename);
        }

        /// <exception cref="IncorrectFileTypeException">Если читатель файла не соответствует типу переданного на чтение файла</exception>
        /// <exception cref="FileNotFoundException">Если по заданному пути не найден файл</exception>
        public void SelectFile(string filename)
        {
            fileName = filename;
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File was not found at {fileName}");
            if (!fileEncoder.IsCompatibleFileType(fileName))
                throw new IncorrectFileTypeException(
                    fileEncoder.GetExpectedFileType(),
                    fileName.Split('.')
                        .LastOrDefault() ?? string.Empty);
        }

        /// <summary>
        /// Позволяет узнать, есть ли ещё слова в файле. Если есть, то указатель перемещается на один вперёд и
        /// возвращается true
        /// </summary>
        /// <returns>true - если ещё есть слова. false - если больше слов нет</returns>
        public bool Next()
        {
            words ??= FillWordsFromFile(fileName, fileEncoder, splitter);
            return ++currentItem < words.Count;
        }
        
        /// <summary>
        /// Позволяет получить текущее слово. Не передвигает указатель. Если вызвать без проверки Next(),
        /// можно получить ошибку
        /// </summary>
        /// <returns>Текущее слово. Либо ошибка, если неправильный вызов</returns>
        public string GetWord()
        {
            if (words == null)
                throw new IncorrectCallException("At first you should call Next()");
            if (currentItem >= words.Count)
                throw new EndOfStreamException("File has not more words");
            return words[currentItem];
        }

        /// <summary>
        /// Метод, который позволяет составить список всех слов в файле.
        /// Разделитель слов и читатель файла передаются как параметры
        /// </summary>
        /// <param name="filename">Полное имя файла, из которого нужно получить слова</param>
        /// <param name="fileEncoder">Читатель, который умеет возвращать весь текст из файла</param>
        /// <param name="splitter">Функция, которая разделяет строку на отдельные слова</param>
        /// <exception cref="ArgumentException">Если не удалось получить данные из файла</exception>
        /// <returns>Список из слов, которые содержатся в файле. Слова в списке повторяются!</returns>
        private static List<string> FillWordsFromFile(string filename, IFileEncoder fileEncoder,
            Func<string, IEnumerable<string>> splitter)
        {
            string text;
            try
            {
                text = fileEncoder.GetText(filename);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Can not get data from file", e);
            }
            return splitter(text).Where(s => s.Length > 0).ToList();
        }
    }
}