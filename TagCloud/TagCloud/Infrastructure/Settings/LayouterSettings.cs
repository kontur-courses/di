namespace TagCloud
{
    public class LayouterSettings
    {
        public float Radius { get; set; }
        public double Step { get; set; }

        public LayouterSettings(float radius, double step)
        {
            Radius = radius;
            Step = step;
        }

        public static LayouterSettings GetDefaultSettings() =>
            new LayouterSettings(0.5f, 0.5f);
    }
}
