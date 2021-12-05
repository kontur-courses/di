using System.Collections.Generic;

namespace TagsCloudVisualization.TextAnalization.LowerCaseMaker
{
    public interface ILowerCaseMaker
    {
        IEnumerable<Word> MakeTextLowerCase(IEnumerable<Word> text);
    }
}
