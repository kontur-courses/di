namespace TagCloud
{
    public class LayouterSettings
    {
        public readonly float Radius = 0.5f;
        public readonly double Step = 0.5f;

        public LayouterSettings(float radius, double step)
        {
            Radius = radius;
            Step = step;
        }

        public static LayouterSettings GetDefaultSettings() =>
            new LayouterSettings(0.5f, 0.5f);
    }
}
