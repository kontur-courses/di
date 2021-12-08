using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualizationDI.Layouter
{
    public class CircularCloudLayouterForRectangles
    {
        private Point Center { get;}

        private Spiral LayouterSpiral { get;}

        private List<Rectangle> RectangleList { get; }


        public CircularCloudLayouterForRectangles(Point center)
        {
            Center = center;
            LayouterSpiral = new Spiral();
            RectangleList = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
           if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
                throw new ArgumentException(); 
           var nextRectangle = CreateNewRectangle(rectangleSize);
           while (RectangleList.Any(rectangle => rectangle.IntersectsWith(nextRectangle))) 
               nextRectangle = CreateNewRectangle(rectangleSize);
           if(nextRectangle.Location != Center)
               nextRectangle = CenterElement(nextRectangle);
           RectangleList.Add(nextRectangle);
           return nextRectangle;
        }

        private Rectangle CreateNewRectangle(Size rectangleSize)
        {
            var rectangleCenterLocation = LayouterSpiral.GetNextPoint(Center);
            var rectangleX = rectangleCenterLocation.X - Math.Abs(rectangleSize.Width / 2);
            var rectangleY = rectangleCenterLocation.Y - Math.Abs(rectangleSize.Height / 2);
            var rectangle = new Rectangle(rectangleX, rectangleY, Math.Abs(rectangleSize.Width), Math.Abs(rectangleSize.Height));
            return rectangle;
        }

        private Rectangle CenterElement(Rectangle inputRectangle)
        { 
            var directionXSign = Math.Sign(Center.X - inputRectangle.X);
            var directionYSign = Math.Sign(Center.Y - inputRectangle.Y);

            while (!IsIntersect(inputRectangle))
            {
                if (inputRectangle.Y == Center.Y)
                    break;
                inputRectangle.Offset(0, directionYSign);
            }
            inputRectangle.Offset(0, -directionYSign);

            while (!IsIntersect(inputRectangle))
            {
                if (inputRectangle.X == Center.X)
                    break;
                inputRectangle.Offset(directionXSign, 0);
            }
            inputRectangle.Offset(-directionXSign, 0);

            inputRectangle.Offset(directionXSign, directionYSign);
            if (IsIntersect(inputRectangle))
                inputRectangle.Offset(-directionXSign, -directionYSign);

            return inputRectangle;
        }

        private bool IsIntersect(Rectangle inputRectangle) =>
            RectangleList.Any(rect => rect.IntersectsWith(inputRectangle));
    }
}
