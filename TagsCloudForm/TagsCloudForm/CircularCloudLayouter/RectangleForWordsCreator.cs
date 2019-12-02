using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;

namespace TagsCloudForm
{
    public class RectangleForWordsCreator : IRectangleForWordsCreator
    {
        public Dictionary<string, Size> CreateRectanglesForWords(Dictionary<string, int> words)
        {
            var outDictionary = new Dictionary<string, Size>();
            foreach (var pair in words)
            {
                //var textBlock = new TextBlock();
                //textBlock.FontSize = pair.Value;
                //textBlock.Text = pair.Key;
                //var size = new Size((int)textBlock.Width+1, (int)textBlock.Height+1);
                var size = new Size(pair.Key.Length * pair.Value, 2*pair.Value);
                outDictionary.Add(pair.Key, size);
            }

            return outDictionary;
        }
    }
}
