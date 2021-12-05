using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface ILiteratureParser
    {
        string ConvertFromLiteratureToProgramReadable(string input, IWordToDefaultFormModifier modifier);

        string FilterWords(IEnumerable<IPartOfSpeech> filter, IPartOfSpeechDeterminator determinator);
    }
}
