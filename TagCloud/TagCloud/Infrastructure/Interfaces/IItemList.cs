using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public interface IItemList<T>
    {
        HashSet<T> SelectedItems { get; set; }
        HashSet<T> AllItems { get; }
    }
}
