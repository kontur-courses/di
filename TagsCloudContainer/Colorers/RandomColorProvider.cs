using System.Drawing;

namespace TagsCloudContainer.Colorers;

public class RandomColorProvider : IColorProvider
{
    
    Random random = new Random();
    public Color ProvideColorForWord(string word, int frequency)
    {
        return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}