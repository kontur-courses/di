using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.App.RectanglesLayouters.Exceptions;

namespace TagsCloudVisualisation.App.RectanglesLayouters.SpiralCloudLayouters
{
    /// <summary>
    /// Класс раскладчика прямоугольников, который рабоатет по приницпу расположения по спирали
    /// </summary>
    public class SpiralCloudLayouter : ICloudLayouter
    {
        private SpiralCloudLayouterSettings? settings;
        private readonly List<Rectangle> setRectangles;
        
        private double rotationAngle;

        public SpiralCloudLayouter()
        {
            rotationAngle = 0d;
            setRectangles = new List<Rectangle>();
        }
        
        public void SetSettings(ICloudLayouterSettings layouterSettings)
        {
            if (layouterSettings == null)
                throw new CloudSettingsException("Layouter settings can not be null");
            if (!(layouterSettings is SpiralCloudLayouterSettings spiralLayouterSettings))
                throw new CloudSettingsException($"Incorrect layouter settings type. " +
                                                 $"Expected {typeof(SpiralCloudLayouterSettings)}, " +
                                                 $"found {layouterSettings.GetType()}");
            settings = spiralLayouterSettings;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (settings == null)
                throw new CloudSettingsException("Layouter settings can not be null");
            Rectangle rectangle;
            if (setRectangles.Count == 0)
                rectangle = new Rectangle(
                    settings.Center.X - rectangleSize.Width / 2,
                    settings.Center.Y - rectangleSize.Height / 2,
                    rectangleSize.Width,
                    rectangleSize.Height);
            else rectangle = FindNextRectangleOnSpiral(rectangleSize);
            setRectangles.Add(rectangle);
            return rectangle;
        }

        /// <summary>
        /// Метод, позволяющий найти расположение следующего на спирали прямоугольника
        /// </summary>
        /// <param name="rectangleSize">Размеры прямоуголььника для добавления</param>
        /// <returns>Прямоугольник, расположенный на плоскости</returns>
        private Rectangle FindNextRectangleOnSpiral(Size rectangleSize)
        {
            rotationAngle = 0d;
            while (true)
            {
                var position = GetNextPositionOnSpiral();
                position.X += settings!.Center.X;
                position.Y += settings!.Center.Y;
                var newRectangle = GetPositionedRectangle_DependedOnAngle(position, rectangleSize);
                if (setRectangles.All(rectangle => !newRectangle.IntersectsWith(rectangle)))
                {
                    return newRectangle;
                }
            }
        }
    
        /// <summary>
        /// Метод, позволяющий получить прямоугольник, позиционированный в зависимости от угла поворота
        /// </summary>
        /// <param name="position">Позиция на спирали</param>
        /// <param name="rectangleSize">Размеры прямоугольника</param>
        /// <returns>Прямоугольник, расположенный нужным образом относительно спирали</returns>
        private Rectangle GetPositionedRectangle_DependedOnAngle(Point position, Size rectangleSize)
        {
            var angle = rotationAngle % (2 * Math.PI);
            var x = position.X;
            var y = position.Y;
            int left, top;
            if (Math.Abs(angle - Math.PI / 2) < 1e-4)
            {
                left = x - rectangleSize.Width / 2;
                top = y - rectangleSize.Height;
                return new Rectangle(left, top, rectangleSize.Width, rectangleSize.Height);
            }
            
            if (Math.Abs(angle - 3 * Math.PI / 2) < 1e-4)
            {
                left = x - rectangleSize.Width / 2;
                top = y;
                return new Rectangle(left, top, rectangleSize.Width, rectangleSize.Height);
            }
            
            var quarterOfPlane = (int)Math.Ceiling(2 * angle / Math.PI);
            switch (quarterOfPlane)
            {
                case 1:
                    left = x;
                    top = y - rectangleSize.Height;
                    break;
                case 2:
                    left = x - rectangleSize.Width;
                    top = y - rectangleSize.Height;
                    break;
                case 3:
                    left = x - rectangleSize.Width;
                    top = y;
                    break;
                default:
                    left = x;
                    top = y;
                    break;
            }
            return new Rectangle(left, top, rectangleSize.Width, rectangleSize.Height);
        }

        /// <summary>
        /// Метод, позволяющий получить следующую целую точку на спирали
        /// </summary>
        private Point GetNextPositionOnSpiral()
        {
            var radius = GetPolarRadiusByAngleOnSpiral(rotationAngle);
            var x = radius * Math.Cos(rotationAngle);
            var y = radius * Math.Sin(rotationAngle);
            var intX = (int)Math.Round(x);
            var intY = (int)Math.Round(y);
            rotationAngle += settings!.RotationStep;
            return new Point(intX, intY);
        }

        /// <summary>
        /// Метод, позволяющий получить удаление от центра в полярной системе координат для спирали,
        /// в зависимости от угла поврота
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns>Удалениие от центра (полярный радиус)</returns>
        private double GetPolarRadiusByAngleOnSpiral(double angle)
        {
            if (angle < 0)
                throw new ArgumentException("angle should be not negative");
            return (settings!.SpiralStep / (2 * Math.PI)) * angle;
        }
    }
}