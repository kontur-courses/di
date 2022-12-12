using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Visualisators
{
    public interface IVisualisator
    {
        public Bitmap Paint(List<Word> words);
    }
}