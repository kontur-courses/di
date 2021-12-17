using System;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public class LogarithmSpiral : ISpiral
    {
        private const double CurveStartingRadius = 0;
        private const double MinimumCurveAngleStep = 0.2;
        private const double CurveAngleStepSlowDown = 0.002;
        private const double DirectionBetweenRoundsCoeff = 1 / (2 * Math.PI);
        private double curveAngleStep = Math.PI / 10;
        private double currentCurveAngle;
        private Point spiralCenter;

        public LogarithmSpiral(Point spiralCenter)
        {
            this.spiralCenter = spiralCenter;
        }

        public Point GetNextCurvePoint()
        {
            currentCurveAngle += curveAngleStep;
            if (curveAngleStep > MinimumCurveAngleStep)
                curveAngleStep -= CurveAngleStepSlowDown;
            return new Point(
                Convert.ToInt32((CurveStartingRadius + DirectionBetweenRoundsCoeff * currentCurveAngle)
                                * Math.Cos(currentCurveAngle)) + spiralCenter.X,
                Convert.ToInt32((CurveStartingRadius + DirectionBetweenRoundsCoeff * currentCurveAngle)
                                * Math.Sin(currentCurveAngle)) + spiralCenter.Y);
        }
    }
}
