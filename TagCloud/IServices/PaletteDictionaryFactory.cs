using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public class PaletteDictionaryFactory : IPaletteDictionaryFactory
    {
        public Dictionary<string, Palette> GetPaletteDictioanry()
        {
            var paletteDictionary = new Dictionary<string, Palette>();
            var shadesOfPinkPallete = new Palette("ShadesOfPink", Color.DeepPink,
                Color.Pink, Color.DarkSalmon, Color.HotPink);
            var shadesOfBlue = new Palette("ShadesOfBlue", Color.Blue, Color.Aqua,
                Color.CornflowerBlue);
            var randomColors = new Palette("RandomColors");
            paletteDictionary[shadesOfPinkPallete.Name] = shadesOfPinkPallete;
            paletteDictionary[shadesOfBlue.Name] = shadesOfBlue;
            paletteDictionary[randomColors.Name] = randomColors;
            return paletteDictionary;
        }
    }
}