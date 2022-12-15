namespace TagCloud.Common.Options;

public class VisualizationOptions
{
    public WordsOptions WordsOptions { get; }
    public DrawingOptions DrawingOptions { get; }
    public SavingOptions SavingOptions { get; }

    public VisualizationOptions(WordsOptions wordsOptions, DrawingOptions drawingOptions, SavingOptions savingOptions)
    {
        WordsOptions = wordsOptions;
        DrawingOptions = drawingOptions;
        SavingOptions = savingOptions;
    }
}