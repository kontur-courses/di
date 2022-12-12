using System.Drawing;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.ImageSaver;
using TagsCloud2.Lemmatizer;
using TagsCloud2.Reader;
using TagsCloud2.TagsCloudMaker;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.SizeDefiner;


namespace TagsCloud2.TagsCloudMakerManager;

public class TagsCloudMakerManager : ITagsCloudMakerManager
{
    private IWordsReader wordsReader;
    private ILemmatizer lemmatizer;
    private IFrequencyCompiler frequencyCompiler;
    private IImageSaver imageSaver;
    private ITagsCloudMaker tagsCloudMaker;
    private IBitmapTagsCloudMaker bitmapTagsCloudMaker;
    private ISizeDefiner sizeDefiner;

    public TagsCloudMakerManager(IWordsReader wordsReader,
        ILemmatizer lemmatizer,
        IFrequencyCompiler frequencyCompiler,
        IImageSaver imageSaver,
        ITagsCloudMaker tagsCloudMaker,
        IBitmapTagsCloudMaker bitmapTagsCloudMaker,
        ISizeDefiner sizeDefiner)
    {
        this.wordsReader = wordsReader;
        this.lemmatizer = lemmatizer;
        this.frequencyCompiler = frequencyCompiler;
        this.imageSaver = imageSaver;
        this.tagsCloudMaker = tagsCloudMaker;
        this.bitmapTagsCloudMaker = bitmapTagsCloudMaker;
        this.sizeDefiner = sizeDefiner;
    }

    public void MakeTagsCloud(string path, 
        string fontFamilyName, 
        Brush colorBrush, 
        string pathToSave,
        string formatToSave,
        bool isVerticalWords,
        int size,
        string pathToExcludingPaths)
    {
        var words = wordsReader.ReadWordsFromFile(path);
        var excludingWordsSet = new HashSet<string>();
        if (pathToExcludingPaths != null)
        {
            var excludingWords = wordsReader.ReadWordsFromFile(pathToExcludingPaths);
            foreach (var word in excludingWords)
            {
                excludingWordsSet.Add(word);
            }
        }

        var imageName = "tagsCloud";
        var normalizeWords = lemmatizer.Lemmatize(words);
        var frequencyDict = frequencyCompiler.GetFrequencyOfWords(normalizeWords, excludingWordsSet);
        var frequencyList = frequencyCompiler.GetFrequencyList(frequencyDict, 100);
        var tagsCloudBitmap = tagsCloudMaker.MakeTagsCloud(frequencyList, fontFamilyName, 50,
            colorBrush, new Size(size, size), bitmapTagsCloudMaker, sizeDefiner, isVerticalWords);
        imageSaver.SaveImage(pathToSave, imageName, formatToSave, tagsCloudBitmap);
        Console.WriteLine($"Готово! Сохранено в файле {pathToSave}{imageName}.{formatToSave}! :)");
    }
}