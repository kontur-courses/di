using System;
using System.Drawing;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGenerator.Core.Spirals
{
    public class ArchimedeanSpiral : ISpiral
    {
        private float phi;
        private float rho;
        private readonly float stepPhi;
        private readonly float k;
        
        public ArchimedeanSpiral(float alpha, float stepPhi)
        {
            phi = 0;
            rho = 0;
            k = (float) (alpha / (2 * Math.PI));
            this.stepPhi = stepPhi;
        }

        public PointF GetNextPoint()
        {
            phi += stepPhi;
            rho = k * phi;
            return PointUtils.FromPolar(rho, phi);
        }
    }
}