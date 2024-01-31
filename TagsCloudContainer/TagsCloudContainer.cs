using TagsCloudContainer.CloudGenerators;
using TagsCloudContainer.FileProviders;
using TagsCloudContainer.TextAnalysers;
using TagsCloudContainer.Visualizers;

namespace TagsCloudContainer;

public class TagsCloudContainer: ITagsCloudContainer
{
    private readonly FileReader fileReader;
    private readonly ICloudVisualizer visualizer;
    private readonly ITagsCloudGenerator cloudGenerator;
    private readonly ITextPreprocessor textPreprocessor;
    private readonly IImageProvider imageProvider;
    
    public TagsCloudContainer(ITagsCloudGenerator cloudGenerator, ICloudVisualizer visualizer, ITextPreprocessor textPreprocessor, FileReader fileReader, IImageProvider imageProvider)
    {
        this.cloudGenerator = cloudGenerator;
        this.visualizer = visualizer;
        this.textPreprocessor = textPreprocessor;
        this.fileReader = fileReader;
        this.imageProvider = imageProvider;
    }

    public void GenerateImageToFile(string inputFile, string outputFile)
    {
        var text = fileReader.ReadFile(inputFile);
        var preprocessedText = textPreprocessor.Preprocess(text);
        var cloud = cloudGenerator.Generate(preprocessedText);
        using var image = visualizer.DrawImage(cloud);
        imageProvider.SaveImage(image, outputFile);
    }
}