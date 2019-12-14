using System;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly PictureConfig pictureConfig;
        private double Parameter => pictureConfig.Parameters.Parameter;
        private double StepInRadians => pictureConfig.Parameters.StepInDegrees * Math.PI / 180;
        private Point Center => pictureConfig.Center;
        
        private double phi;

        public ArchimedeanSpiral(PictureConfig pictureConfig)
        {
            this.pictureConfig = pictureConfig;
        }

        public Point GetNextPoint()
        {
            var r = Parameter * phi;
            var point =  GeometryUtils.ConvertPolarToIntegerCartesian(r, phi);
            phi += StepInRadians;
            return new Point(point.X + Center.X, point.Y + Center.Y);
        }
    }
}