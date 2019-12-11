using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface IPaletteDictionaryFactory
    {
        Dictionary<string, Palette> GetPaletteDictioanry();
    }
}