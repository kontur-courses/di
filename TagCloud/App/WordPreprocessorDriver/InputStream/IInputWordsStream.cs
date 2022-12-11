using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream;

/// <summary>
/// Интерфейс, который позволяет получать слова по одному из какого-либо источника
/// </summary>
public interface IInputWordsStream
{
    IInputWordsStream OpenFile(string filename, IFileEncoder selectedFileEncoder);
        
    IInputWordsStream UseSplitter(ITextSplitter textSplitter);
        
    /// <summary>
    /// Позволяет узнать, есть ли ещё слова в файле. Если есть, то указатель перемещается на один вперёд и
    /// возвращается true
    /// </summary>
    /// <returns>true - если ещё есть слова. false - если больше слов нет</returns>
    bool MoveNext();
        
    /// <summary>
    /// Позволяет получить текущее слово. Не передвигает указатель. Если вызвать без проверки Next(),
    /// можно получить ошибку
    /// </summary>
    /// <returns>Текущее слово. Либо ошибка, если неправильный вызов</returns>
    string GetWord();
}