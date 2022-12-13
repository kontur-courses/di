using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultDrawer : IDrawer
    {
        private IInputTextProvider inputTextProvider;
        private Settings settings;
        private IRectangleArranger rectangleArranger;

        public DefaultDrawer(IInputTextProvider inputTextProvider, ISettingsProvider settingsProvider,
            IRectangleArranger rectangleArranger)
        {
            this.inputTextProvider = inputTextProvider;
            this.settings = settingsProvider.Settings;
            this.rectangleArranger = rectangleArranger;
        }

        public Bitmap DrawImage()
        {
            var textContainers = rectangleArranger.GetContainers(inputTextProvider.GetWords(), settings);
            var radius = GetRadius(textContainers) + 100;
            var image = new Bitmap(radius * 2, radius * 2);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(settings.BackgroundColor);
            foreach (var container in textContainers)
            {
                var x = container.Rectangle.X + radius;
                var y = container.Rectangle.Y + radius;
                graphics.DrawString(container.Text, container.Font, new SolidBrush(settings.FontColor), x, y);
            }

            return image;
        }

        private int GetRadius(List<TextContainer> containers)
        {
            int radius = 0;
            foreach (var container in containers)
            {
                radius = Math.Max(radius, Math.Abs(container.Rectangle.Bottom));
                radius = Math.Max(radius, Math.Abs(container.Rectangle.Top));
                radius = Math.Max(radius, Math.Abs(container.Rectangle.Right));
                radius = Math.Max(radius, Math.Abs(container.Rectangle.Left));
            }

            return radius;
        }
    }
}