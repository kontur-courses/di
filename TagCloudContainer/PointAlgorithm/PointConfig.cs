namespace TagCloudContainer.PointAlgorithm
{
    public class PointConfig : IPointConfig
    {
        public PointConfig(double ellipsoidMultiplier, double densityMultiplier)
        {
            EllipsoidMultiplier = ellipsoidMultiplier;
            DensityMultiplier = densityMultiplier;
        }
        public double EllipsoidMultiplier { get; set; }
        public double DensityMultiplier { get; set; }
    }
}
