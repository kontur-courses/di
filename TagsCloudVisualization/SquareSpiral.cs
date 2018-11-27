using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class SquareSpiral : ISpiral
    {
        public SquareSpiral(Point center)
        {
            this.center = center;
        }

        private readonly Point center;
        private double angle = 0;

        private const double SpiralShift = 1;
        private const double AngleShift = 0.05;

        private Point GetCurrentPositionOnTheSpiral()
        {
            var a = angle % (Math.PI * 2);

            double x, y;
            if (a < Math.PI)
            {
                if (a > Math.PI / 2)
                {
                    if (a < Math.PI / 4)
                    {
                        x = center.X + (SpiralShift * angle * GetSmthCos(angle));
                        y = center.Y + (SpiralShift * angle * Math.Sin(angle));
                    }
                    else
                    {
                        x = center.X + (SpiralShift * angle * Math.Cos(angle));
                        y = center.Y + (SpiralShift * angle * GetSmthSin(angle));
                    }
                }
                else
                {
                    if (a < Math.PI * 3 / 4)
                    {
                        x = center.X + (SpiralShift * angle * Math.Cos(angle));
                        y = center.Y + (SpiralShift * angle * GetSmthSin(angle));
                    }
                    else
                    {
                        x = center.X + (SpiralShift * angle * GetSmthCos(angle));
                        y = center.Y + (SpiralShift * angle * Math.Sin(angle));
                    }
                }
            }


            else
            {
                a -= Math.PI;
                if (a > Math.PI / 2)
                {
                    if (a > Math.PI / 4)
                    {
                        x = center.X + (SpiralShift * angle * GetSmthCos(angle));
                        y = center.Y + (SpiralShift * angle * Math.Sin(angle));
                    }
                    else
                    {
                        x = center.X + (SpiralShift * angle * Math.Cos(angle));
                        y = center.Y + (SpiralShift * angle * GetSmthSin(angle));
                    }
                }
                else
                {
                    if (a < Math.PI * 3 / 4)
                    {
                        x = center.X + (SpiralShift * angle * GetSmthCos(angle));
                        y = center.Y + (SpiralShift * angle * Math.Sin(angle));
                    }
                    else
                    {
                        x = center.X + (SpiralShift * angle * Math.Cos(angle));
                        y = center.Y + (SpiralShift * angle * GetSmthSin(angle));
                    }
                }
            }
            return new Point((int)x, (int)y);
            //var x = center.X + (SpiralShift * angle * GetSmthCos(angle));
            //var y = center.Y + (SpiralShift * angle * GetSmthSin(angle));

            //if (Math.Sign(Math.Cos(angle) * Math.Sin(angle)) == 1)
            //{
            //    var x = center.X + (SpiralShift * angle * Math.Sign(Math.Cos(angle)));
            //    var y = center.Y + (SpiralShift * angle * Math.Sin(angle));
            //    return new Point((int)x, (int)y);
            //}
            //else if (Math.Sign(Math.Cos(angle) * Math.Sin(angle)) == -1)
            //{
            //    var x = center.X + (SpiralShift * angle * Math.Cos(angle));
            //    var y = center.Y + (SpiralShift * angle * Math.Sign(Math.Sin(angle)));
            //    return new Point((int)x, (int)y);
            //}
            //throw new ArgumentException();
            //if (a > Math.PI / 4)
            //{
            //    var x = center.X + (SpiralShift * angle * Math.Sign(Math.Cos(angle)));
            //    var y = center.Y + (SpiralShift * angle * Math.Sin(angle));
            //    return new Point((int)x, (int)y);
            //}
            //else
            //{
            //    var x = center.X + (SpiralShift * angle * Math.Cos(angle));
            //    var y = center.Y + (SpiralShift * angle * Math.Sign(Math.Sin(angle)));
            //    return new Point((int)x, (int)y);
            //}

        }

        public int GetSmthSin(double angle)
        {
            var a = angle % (Math.PI * 2);
            switch ((int)(a / (Math.PI / 4)))
            {
                case 1:
                case 2:
                    return 1;
                case 5:
                case 6:
                    return -1;
                default:
                    return 0;
            }
        }
        public int GetSmthCos(double angle)
        {
            var a = angle % (Math.PI * 2);
            switch ((int)(a / (Math.PI / 4)))
            {
                case 0:
                case 7:
                    return 1;
                case 3:
                case 4:
                    return -1;
                default:
                    return 0;
            }
        }

        public Rectangle GetRectangleInNextLocation(Size rectangleSize)
        {
            angle += AngleShift;
            var rectangle = new Rectangle(GetCurrentPositionOnTheSpiral(), rectangleSize);

            return rectangle.ShiftCoordinatesToCenterRectangle();
        }
    }
}
