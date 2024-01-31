using ConsoleApp.Options;
using MyStemWrapper;
using TagsCloudContainer;
using TagsCloudContainer.Settings;

namespace ConsoleApp.Handlers;

public class GenerateCloudOptionsHandler : IOptionsHandler
{
    private readonly MyStem myStem;
    private readonly IAppSettings appSettings;
    private readonly IAnalyseSettings analyseSettings;
    private readonly ITagsCloudContainer cloudContainer;

    public GenerateCloudOptionsHandler(IAppSettings appSettings, MyStem myStem, IAnalyseSettings analyseSettings,
        ITagsCloudContainer cloudContainer)
    {
        this.appSettings = appSettings;
        this.myStem = myStem;
        this.analyseSettings = analyseSettings;
        this.cloudContainer = cloudContainer;
    }

    public bool CanParse(object options)
    {
        return options is GenerateCloudOptions;
    }

    public string WithParsed(object options)
    {
        Map(options);
        return Execute();
    }

    private void Map(object options)
    {
        if (options is GenerateCloudOptions opts)
            Map(opts);
        else
            throw new ArgumentException(nameof(options));
    }
    
    private void Map(GenerateCloudOptions options)
    {
        appSettings.InputFile = options.InputFile;
        appSettings.OutputFile = options.OutputFile;

        if (!string.IsNullOrWhiteSpace(options.AnalyseParameters))
            myStem.Parameters = "-" + options.AnalyseParameters;
        if (options.ValidSpeechParts.Any())
            analyseSettings.ValidSpeechParts = options.ValidSpeechParts.ToArray();
    }

    private string Execute()
    {
        cloudContainer.GenerateImageToFile(appSettings.InputFile, appSettings.OutputFile);
        return $"Успешно сохранено в файл - \"{appSettings.OutputFile}\".";
    }
}