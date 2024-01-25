using ConsoleApp.CommandLineParsers.Options;
using MyStemWrapper;
using TagsCloudContainer;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextAnalysers;

namespace ConsoleApp.CommandLineParsers.Handlers;

public class PreprocessTextOptionsHandler: IOptionsHandler<PreprocessTextOptions>
{
    private readonly ITextPreprocessor textPreprocessor;
    private readonly FileReader fileReader;
    private readonly CloudData cloudData;
    private readonly MyStem myStem;

    public PreprocessTextOptionsHandler(ITextPreprocessor textPreprocessor, FileReader fileReader, CloudData cloudData, MyStem myStem)
    {
        this.textPreprocessor = textPreprocessor;
        this.fileReader = fileReader;
        this.cloudData = cloudData;
        this.myStem = myStem;
    }

    public void Map(PreprocessTextOptions options)
    {
        cloudData.FilePath = options.FilePath;
        if (options.Parameters is not null)
            myStem.Parameters = "-" + options.Parameters;
    }

    public void Map(object options)
    {
        if (options is PreprocessTextOptions opts)
            Map(opts);
        else
            throw new ArgumentException(nameof(options));
    }

    public void Execute()
    {
        var text = fileReader.ReadFile();
        textPreprocessor.Preprocess(text);
    }
}