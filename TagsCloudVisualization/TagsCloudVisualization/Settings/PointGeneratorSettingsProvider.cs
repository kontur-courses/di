namespace TagsCloudVisualization.Settings
{
    public class PointGeneratorSettingsProvider
    {
        public static PointGeneratorSettings GetDefaultSettings()
        {
            return new PointGeneratorSettings
            {
                DegreeStep = 0.087,
                FactorStep = 0.2
            };
        }
    }
}