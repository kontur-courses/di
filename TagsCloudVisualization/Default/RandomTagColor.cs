using System;
using System.Drawing;

namespace TagsCloudVisualization.Default
{
    public class RandomTagColor : ITokenColorChooser
    {
        public Color GetTokenColor(Token token)
        {
            //var randomGen = new Random((int)token.Weight);
            var randomGen = new Random(token.Value.GetHashCode());
            return Color.FromArgb(randomGen.Next(0,200), randomGen.Next(0,200), randomGen.Next(0,200));
        }
    }
}