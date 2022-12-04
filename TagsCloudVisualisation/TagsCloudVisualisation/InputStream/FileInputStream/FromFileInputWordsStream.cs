using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.InputStream.FileInputStream
{
    /// <summary>
    /// Класс, предоставляющий возможность организовать поток слов из файла
    /// </summary>
    public class FromFileInputWordsStream : IInputWordsStream
    {
        private readonly string fileName;
        private readonly IFileEncoder fileEncoder;
        private readonly Func<string, IEnumerable<string>> splitter;
        
        private List<string>? words;
        private int currentItem = -1;
        
        /// <param name="fileName">Полное имя файла, из которого производится чтение слов</param>
        /// <param name="fileEncoder">Обработчик, который умеет получать текст из файла</param>
        /// <param name="splitter">Функция, которая умеет разделять текст на отдельные слова</param>
        public FromFileInputWordsStream(string fileName, IFileEncoder fileEncoder, Func<string, IEnumerable<string>> splitter)
        {
            this.fileName = fileName;
            this.fileEncoder = fileEncoder;
            this.splitter = splitter;
        }
        
        /// <summary>
        /// Позволяет узнать, есть ли ещё слова в файле. Если есть, то указатель перемещается на один вперёд и
        /// возвращается true
        /// </summary>
        /// <returns>true - если ещё есть слова. false - если больше слов нет</returns>
        public bool Next()
        {
            words ??= FillWordsFromFile(fileName, fileEncoder, splitter);
            return ++currentItem < words!.Count;
        }
        
        /// <summary>
        /// Позволяет получить текущее слово. Не передвигает указатель. Если вызвать без проверки Next(),
        /// можно получить ошибку
        /// </summary>
        /// <returns>Текущее слово. Либо ошибка, если неправильный вызов</returns>
        public string GetWord()
        {
            return words![currentItem];
        }

        /// <summary>
        /// Метод, который позволяет составить список всех слов в файле.
        /// Разделитель слов и читатель файла передаются как параметры
        /// </summary>
        /// <param name="filename">Полное имя файла, из которого нужно получить слова</param>
        /// <param name="fileEncoder">Читатель, который умеет возвращать весь текст из файла</param>
        /// <param name="splitter">Функция, которая разделяет строку на отдельные слова</param>
        /// <returns>Список из слов, которые содержатся в файле. Слова в списке повторяются!</returns>
        private static List<string>? FillWordsFromFile(string filename, IFileEncoder fileEncoder,
            Func<string, IEnumerable<string>> splitter)
        {
            var text = fileEncoder.GetText(filename);
            return splitter(text).ToList();
        }
    }
}