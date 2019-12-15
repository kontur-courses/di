using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.CircularCloudLayouterSettings
{
    public interface ICircularCloudLayouterWithWordsSettings
    {
        int CenterX { get; set; }

        int CenterY { get; set; }

        int Scale { get; set; }

        string WordsSource { get; set; }

        string BoringWordsFile { get; set; }

        string PartOfSpeechToFilterFile { get; set; }

        bool Ordered { get; set; }

        bool UpperCase { get; set; }

        bool Frame { get; set; }

        bool Fill { get; set; }


        LanguageEnum Language { get; set; }
    }
}
