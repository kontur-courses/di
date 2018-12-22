using System;

namespace WordCloudImageGenerator.Parsing.Word
{
    public interface IWord : IComparable<IWord>
    {
        string Text { get; }
        int Entries { get; }
    }
}