using System;
using System.Drawing;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SqrtLengthSizeSelector : ISizableSelector
    {
        public static Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));

        public Size GetSize(string word, int count, DrawerSettings settings)
        {
            return (Graphics.MeasureString(word, settings.TextFont) * (MathF.Log(count)*word.Length + 1)).ToSize();
        }
    }
}