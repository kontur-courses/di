using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualisation.Coloring
{
    public class MonoColorManager : IColorManager
    {
        private readonly Color color;

        public MonoColorManager(Color color)
        {
            this.color = color;
        }

      
        public Dictionary<TagsCloudWord, Color> GetWordsColors(List<TagsCloudWord> words)
        {
            var result = new Dictionary<TagsCloudWord, Color>();
            foreach (var word in words)
            {
                result[word] = color;
            }

            return result;
        }
    }
}