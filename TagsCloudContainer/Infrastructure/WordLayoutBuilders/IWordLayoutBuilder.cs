using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordLayoutBuilders
{
    public interface IWordLayoutBuilder
    {
        public IWordLayoutBuilder AddWord(string word, SizeF size);
        public WordRectangle[] Build(PointF center);
    }
}