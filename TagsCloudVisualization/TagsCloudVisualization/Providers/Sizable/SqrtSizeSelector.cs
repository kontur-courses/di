using System;
using System.Drawing;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SqrtSizeSelector : ISizableSelector
    {
        public static Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));

        public Size GetSize(string word, int count, DrawerSettings settings)
        {
            return (Graphics.MeasureString(word, settings.TextFont) * (MathF.Sqrt(count) + 1)).ToSize();
        }
    }
}