namespace TagsCloudVisualization.Settings
{
    public class PointGeneratorSettings : IPointGeneratorSettingsProvider
    {
        public double DegreeStep { get; set; } = 0.087;
        public double FactorStep { get; set; } = 0.2;
    }
}
