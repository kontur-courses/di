namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class LayouterParameters
    {
        public double Parameter { get; set; }
        public double StepInDegrees { get; set; }

        public LayouterParameters()
        {
            Parameter = 0.25;
            StepInDegrees = 1;
        }
    }
}
