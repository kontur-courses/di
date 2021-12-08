using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public static class ElementCentering
    {
        public static Rectangle Centering(Rectangle rectangleElement, 
            Point center, Dictionary<string, RectangleWithWord> rectangleDict)
        {
            var directionXSign = Math.Sign(center.X - rectangleElement.X);
            var directionYSign = Math.Sign(center.Y - rectangleElement.Y);
            while (!IsIntersect(rectangleElement, rectangleDict))
            {
                if (rectangleElement.Y == center.Y)
                    break;
                rectangleElement.Offset(0, directionYSign);
            }
            rectangleElement.Offset(0, -directionYSign);

            while (!IsIntersect(rectangleElement, rectangleDict))
            {
                if (rectangleElement.X == center.X)
                    break;
                rectangleElement.Offset(directionXSign, 0);
            }
            
            rectangleElement.Offset(-directionXSign, 0);

            if (rectangleDict.Count == 0)
                rectangleElement.Offset(directionXSign, directionYSign);
            if (IsIntersect(rectangleElement, rectangleDict))
                rectangleElement.Offset(-directionXSign, -directionYSign);

            return rectangleElement;
        }
        private static  bool IsIntersect(Rectangle inputRectangle, Dictionary<string, RectangleWithWord> rectangleDict) =>
            rectangleDict.Select(el => el.Value)
                .Any(rect => rect.RectangleElement.IntersectsWith(inputRectangle));
    }
}
