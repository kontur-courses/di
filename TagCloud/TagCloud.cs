using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using TagCloud.PointGetters;
using System.Collections;

namespace TagCloud
{
    public class TagCloud : IEnumerable<(string word, Rectangle location)>
    {
        internal readonly CircularCloudLayouter layouter;
        private readonly Dictionary<string, double> wordsMetric;
        private readonly (string word, Rectangle location)[] locations;

        internal TagCloud(Dictionary<string, double> wordsMetric, IPointGetter getter)
        {
            this.wordsMetric = wordsMetric;
            layouter = new CircularCloudLayouter(getter);
            locations = GetLocations();
        }

        public IEnumerator<(string word, Rectangle location)> GetEnumerator()
        {
            foreach (var w in locations)
                yield return w;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private (string word, Rectangle location)[] GetLocations() =>
            wordsMetric
            .OrderByDescending(w => w.Value)
            .Select(w => GetWordLocation(w))
            .ToArray();

        private (string word, Rectangle location) GetWordLocation(KeyValuePair<string, double> word)
        {
            var size = new Size(10 * word.Key.Length * (int)word.Value, 10 * (int)word.Value);
            var location = layouter.PutNextRectangle(size);
            return (word.Key, location);
        }
    }
}
