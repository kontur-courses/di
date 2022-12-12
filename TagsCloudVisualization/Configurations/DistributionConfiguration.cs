namespace TagsCloudVisualization.Configurations
{
    public class DistributionConfiguration
    {
        public static DistributionConfiguration Default => 
            new DistributionConfiguration(0.1f, 5.0f, 2.5f);
        
        public float ShiftAngle { get; }
        public float ShiftX { get; }
        public float ShiftY { get; }

        public DistributionConfiguration(float shiftAngle, float shiftX, float shiftY)
        {
            ShiftAngle = shiftAngle;
            ShiftX = shiftX;
            ShiftY = shiftY;
        }
    }
}