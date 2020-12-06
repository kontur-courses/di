﻿using System;
using System.ComponentModel;
using System.Drawing;
using TagsCloudLayouters.Contracts;

namespace TagsCloudLayouters.Core.ColorizeAlgorithms
{
    public class SteelForHumans : IColorizer
    {
        private readonly Random rnd = new Random();
        [Browsable(false)] public string Name => "Steel for humans";

        public Color Paint()
        {
            var grey = rnd.Next(255);
            return Color.FromArgb(grey, grey, grey);
        }
    }
}