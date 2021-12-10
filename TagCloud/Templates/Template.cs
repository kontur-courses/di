using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.TextHandlers;

namespace TagCloud.Templates
{
    public class Template : ITemplate
    {
        private readonly List<WordParameter> words = new();
        public Size Size { get; set; }
        public Color BackgroundColor { get; set; }

        public bool IsEmpty()
        {
            return words.Count == 0;
        }

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