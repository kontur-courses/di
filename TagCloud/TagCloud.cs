using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using TagCloud.PointGetters;
using System.Collections;

namespace TagCloud
{
    internal class TagCloud : IEnumerable<(string word, Rectangle location)>
    {
        private readonly CircularCloudLayouter layouter;
        private readonly Dictionary<string, double> wordsMetric;

        internal TagCloud(Dictionary<string, double> wordsMetric, IPointGetter getter)
        {
            this.wordsMetric = wordsMetric;
            layouter = new CircularCloudLayouter(getter, Point.Empty);
        }

        public IEnumerator<(string word, Rectangle location)> GetEnumerator()
        {
            var result = wordsMetric
                .OrderBy(w => w.Value)
                .Select(w => GetWordLocation(w));
            foreach (var w in result)
                yield return w;

            (string word, Rectangle location) GetWordLocation(KeyValuePair<string, double> word)
            {
                var size = new Size((int)word.Value, word.Key.Length * (int)word.Value);
                var location = layouter.PutNextRectangle(size);
                return (word.Key, location);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
