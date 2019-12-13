using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SizableSelector : ISizableSelector<string, int>
    {
        public static Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));

        public Size GetSize(string obj, int info, DrawerSettings settings)
        {
            return (Graphics.MeasureString(obj, settings.TextFont) * (MathF.Log(info) + 1)).ToSize();
        }
    }
}