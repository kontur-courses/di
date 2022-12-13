using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualization
{
    public interface IPainter
    {
        public void DrawWordsToFile(List<Word> words, string path);

    }
}
