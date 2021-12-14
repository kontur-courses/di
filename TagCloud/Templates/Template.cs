using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Templates
{
    public class Template : ITemplate
    {
        private readonly List<WordParameter> words = new();
        public Size Size { get; set; }
        public Color BackgroundColor { get; set; }

        public PointF Center { get; set; }

        public Template()
        {
        }

        public Template(IEnumerable<WordParameter> words)
        {
            this.words = words.ToList();
        }

        public void Add(WordParameter wordParameter)
        {
            words.Add(wordParameter);
        }

        public IEnumerable<WordParameter> GetWordParameters()
        {
            return words;
        }
    }
}