using System.Drawing;

namespace TagsCloud.Distributors;

public class SpiralDistributor:IDistributor
{

        private double Angle { get;  set; }
        private double AngleStep { get; set; }
        private double RadiusStep { get; set; }
        private Point Center { get; set; }
        private double Radius { get; set; }
        
        public SpiralDistributor(Point center = new Point(), double angleStep = 0.1, double radiusStep = 0.1)
        {
            if (radiusStep <= 0 || angleStep == 0) throw new ArgumentException();
            Center = center;
            Radius = 0;
            Angle = 0;
            AngleStep = angleStep - 2 * Math.PI * (int)(angleStep / 2 * Math.PI);
            RadiusStep = radiusStep;
        }


        public Point GetNextPosition()
        {
            var x = Radius * Math.Cos(Angle) + Center.X;
            var y = Radius * Math.Sin(Angle) + Center.Y;
            
            Angle += AngleStep;
            
            if (Angle >= Math.PI * 2)
            {
                Angle -= 2 * Math.PI;
                Radius += RadiusStep;
            }

            return new Point((int)x, (int)y);
        }
}