using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CommandLine;
using TagCloudUsageSample.Validators;
using TagsCloudVisualization;

namespace TagCloudUsageSample
{
    public class ClTextOptions : BaseOptions
    {
        [FileValidator("invalid file name")]
        [Option('t', "text", Default = @"sample.txt", HelpText = "Text file path")]
        public string TextFilePath { get; private set; }
        
        [PathValidator("unknown directory")]
        [Option('p', "path", Default = @"..\..\", HelpText = "Set path to save tag clouds.")]
        public string SavePath{ get; private set; }
        
        [FileNameValidator("invalid file name")]
        [Option('n', "name", Default = "TagCloud", HelpText = "Set name to save tag clouds.")]
        public string FileName { get; private set; }
        
        [FileValidator("invalid file name")]
        [Option('i', "ignore", HelpText = "Ignore words file path")]
        public string IgnoreWordsFileName { get; private set; }
        
        [Option('o', "open", Default = true, HelpText = "Open created file")]
        public bool Open { get; set; }
        
        [Option('l', "isLiteraryText", Default = true, HelpText = "Is text literary")]
        public bool IsLiteraryText { get; set; }
        
        [RangeValidator(1, 50, nameof(Density))]
        [Option('d', "density", Default = 5, HelpText = "Set density")]
        public int Density { get; private set; }
        
        [RangeValidator(1, 50, nameof(MinWordLengthToStatistic))]
        [Option('m', "wordLength", Default = 3, HelpText = "Set min word length to statistic")]
        public int MinWordLengthToStatistic { get; private set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Set font for words.")]
        public string Font { get; private set; }

        [RangeValidator(-1, 500, nameof(WordCountToStatistic))]
        [Option('w', "wordCount", Default = -1, HelpText = "Set number of word to statistic.")]
        public int WordCountToStatistic { get; private set; }
        
        [ColorValidator("incorrect color")]
        [Option('c', "color", Default = null, HelpText = "Set color of words.")]
        public string Color { get; set; }
        
        [SizeValidator(1, 10000, nameof(Size))]
        [Option('s', "size", Default = "900 900", HelpText = "Set size of image.")]
        public string Size { get; set; }
        
        private readonly TagCloud tagCloud = new();
        
        public void CreateTags(out string firstFileName)
        {
            firstFileName = Path.Combine(SavePath, FileName) + "." + ImageFormat.Png.ToString().ToLower();
            tagCloud.GetBitmap(GetConfig()).Save(firstFileName, ImageFormat.Png);
        }

        private Config GetConfig()
        {
            var splittedColor = Color?.Split(' ').Select(int.Parse).ToList();
            var size = Size.Split(' ').Select(int.Parse).ToList();
            
            return new Config
            {
                Size = new Size(size[0], size[1]),
                Color = Color is null ? null : System.Drawing.Color.FromArgb(splittedColor[0], splittedColor[1], splittedColor[2]),
                WordCountToStatistic = WordCountToStatistic,
                Density = Density,
                MinWordToStatisticLength = (byte)MinWordLengthToStatistic,
                Font = Font,
                TextFilePath = TextFilePath,
                CustomIgnoreFilePath = IgnoreWordsFileName,
                TagCloudResultActions = Open ? TagCloudResultActions.SaveAndOpen : TagCloudResultActions.Save,
                SourceTextInterpretationMode = IsLiteraryText ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine
            };
        }
    }
}