using System;
using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Algorithms
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly ICloudSettings cloudSettings;
        private double spiralAngle;

        public ArchimedeanSpiral(ICloudSettings cloudSettings)
        {
            this.cloudSettings = cloudSettings;
        }

        public Point GetNextPoint()
        {
            var x = cloudSettings.CenterPoint.X + (int)(spiralAngle * Math.Cos(spiralAngle));
            var y = cloudSettings.CenterPoint.Y + (int)(spiralAngle * Math.Sin(spiralAngle));

            spiralAngle++;

            return new Point(x,y);
        }

        public double GetCurrentSpiralAngle() => spiralAngle;
    }
}
