using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextAnalization.LowerCaseMaker
{
    public interface ILowerCaseMaker
    {
        IEnumerable<Word> MakeTextLowerCase(IEnumerable<Word> text);
    }
}
