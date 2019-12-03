using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public class CircularCloudLayouterWithWordsSettings
    {
        public int CenterX { get; set; } = 200;

        public int CenterY { get; set; } = 200;

        public int Scale { get; set; } = 5;

        public string WordsSource { get; set; } = "words.txt";

        public bool Ordered { get; set; } = false;

    }
}
