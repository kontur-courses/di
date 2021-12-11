using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class OvalSpiral : ISpiral
    {
        private readonly double spiralCoef;
        private readonly double angleDelta;
        private readonly Settings settings;
        private double currentAngle;

        public OvalSpiral(Settings settings)
        {
            this.settings = settings;
            spiralCoef = 1;
            angleDelta = Math.PI / 360;
        }

        public Point GetNext()
        {
            var center = settings.Center;
            var x = Math.Round(2 * spiralCoef * currentAngle * Math.Cos(currentAngle)) + center.X;
            var y = Math.Round(spiralCoef * currentAngle * Math.Sin(currentAngle)) + center.Y;
            currentAngle += angleDelta;
            return new Point((int)x, (int)y);
        }

        public void Reset()
        {
            currentAngle = 0;
        }
    }
}
