using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class DefinerSize
    {

        private readonly FontSettings fontSettings;

        public DefinerSize(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }


        public Dictionary<string, Font> DefineFontSize(IReadOnlyDictionary<string, int> d)
        {
            var countWord = d.Values.Max();
            var result = new Dictionary<string, Font>();
            var difference = fontSettings.MaxEmSize - fontSettings.MinEmSize;

            foreach (var word in d.Keys)
            {
                var percent = d[word] / (float)countWord;
                var emSize =  fontSettings.MinEmSize +  difference * percent;
                result[word] = new Font(fontSettings.FontFamily, emSize, fontSettings.Style);
            }
            return result;
        }
    }
}