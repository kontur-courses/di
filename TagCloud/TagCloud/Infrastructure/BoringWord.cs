using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class BoringWord : ICheckable
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public BoringWord(string name)
        {
            IsChecked = true;
            Name = name;
        }
    }
}
