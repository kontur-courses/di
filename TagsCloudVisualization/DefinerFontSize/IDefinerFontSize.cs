using System.Collections.Generic;
using TagsCloudVisualization.Infrastructure.Analyzer;

namespace TagsCloudVisualization.DefinerFontSize
{
    public interface IDefinerFontSize
    {
        IEnumerable<WordWithFont> DefineFontSize(IEnumerable<IWeightedWord> words);
    }
}