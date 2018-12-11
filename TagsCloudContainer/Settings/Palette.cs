using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FluentAssertions.Common;
using TagsCloudContainer.Painter;

namespace TagsCloudContainer.Settings
{
    public class Palette
    {
        public Color PrimaryColor { get; set; } = Color.DarkOrange;
        public Color BackgroundColor { get; set; } = Color.Black;
    }
}