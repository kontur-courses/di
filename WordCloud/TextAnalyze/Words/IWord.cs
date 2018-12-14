using System;

namespace WordCloud.TextAnalyze.Words
{
    public interface IWord : IComparable<IWord>
    {
        string Text { get; }
        int Entries { get; }
    }
}