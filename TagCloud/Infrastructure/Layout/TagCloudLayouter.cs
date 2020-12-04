using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Layout
{
    // ReSharper disable IdentifierTypo
    public class TagCloudLayouter : ILayouter<Size, Rectangle>
    {
        private readonly Func<ITagCloudSettingsProvider> tagCloudSettingsProvider;
        private readonly IEnvironment<Rectangle> environment;
        private readonly ILayoutStrategy strategy;

        public TagCloudLayouter(IEnvironment<Rectangle> environment, ILayoutStrategy strategy, Func<ITagCloudSettingsProvider> tagCloudSettingsProvider)
        {
            this.tagCloudSettingsProvider = tagCloudSettingsProvider;
            this.environment = environment;
            this.strategy = strategy;
        }

        public Rectangle GetPlace(Size rectangleSize)
        {
            var rectangleMiddle = new Size(rectangleSize.Width / 2, rectangleSize.Height / 2);

            var possiblePoint = strategy.GetPoint(point => CanPlaceRectangle(point - rectangleMiddle, rectangleSize));
            var rectangle = new Rectangle(possiblePoint - rectangleMiddle, rectangleSize);
            
            environment.Add(rectangle);
            return rectangle;
        }

        private bool CanPlaceRectangle(Point possiblePoint, Size rectangleSize)
        {
            var rectangle = new Rectangle(possiblePoint, rectangleSize);
            return !environment.IsColliding(rectangle);
        }
    }
}