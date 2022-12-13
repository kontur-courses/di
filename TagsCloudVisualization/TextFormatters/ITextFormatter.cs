using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextFormatters
{
    public interface ITextFormatter
    {
        public List<Word> Format(string text);
    }
}
