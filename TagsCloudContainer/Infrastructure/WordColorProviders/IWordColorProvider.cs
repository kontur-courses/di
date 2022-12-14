using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordColorProviders
{
    public interface IWordColorProvider
    {
        public Color GetColor(string word);
    }
}