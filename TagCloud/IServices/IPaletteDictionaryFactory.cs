using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface IPaletteDictionaryFactory
    {
        Dictionary<string, Palette> GetPaletteDictioanry();
    }
}
