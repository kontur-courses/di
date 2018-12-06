namespace TagsCloudVisualization.Settings
{
    public interface IPointGeneratorSettingsProvider
    {
        double DegreeStep { get; set; }
        double FactorStep { get; set; }
    }
}
