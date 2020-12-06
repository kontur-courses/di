using System;
using System.ComponentModel;
using System.Drawing;
using TagsCloudLayouters.Contracts;

namespace TagsCloudLayouters.Core.ColorizeAlgorithms
{
    public class RandomColorizer : IColorizer
    {
        private readonly Random rnd = new Random();

        [Browsable(false)] public string Name => "Random colorizer";
        public Color Paint() => Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
    }
}