using System;
using System.Drawing;
using System.Linq;
using CommandLine;
using ResultProject;
using TagCloudUsageSample.Validators;
using TagsCloudVisualization;

namespace TagCloudUsageSample
{
    public class ClTextOptions
    {
        [FileValidator("invalid file name")]
        [Option('t', "text", Default = @"sample.txt", HelpText = "Text file path")]
        public string TextFilePath { get; private set; }
        
        [PathValidator("unknown directory")]
        [Option('p', "path", Default = @"..\..\", HelpText = "Set path to save tag clouds.")]
        public string SavePath{ get; private set; }
        
        [FileNameValidator("invalid file name")]
        [Option('n', "name", Default = "TagCloud", HelpText = "Set name to save tag clouds.")]
        public string SaveFileName { get; private set; }
        
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

        [RangeValidator(0, 500, nameof(WordCountToStatistic))]
        [Option('w', "wordCount", Default = 50, HelpText = "Set number of word to statistic.")]
        public int WordCountToStatistic { get; private set; }
        
        [ColorValidator("incorrect color")]
        [Option('c', "color", Default = null, HelpText = "Set color of words.")]
        public string Color { get; set; }
        
        [SizeValidator(1, 10000, nameof(Size))]
        [Option('s', "size", Default = "900 900", HelpText = "Set size of image.")]
        public string Size { get; set; }

        private Result<bool> ValidateRanges()
        {
            return Result.Ok(GetType())
                .Then(x => x.GetProperties())
                .Then(x => x.Where(y => y.GetCustomAttributes(true).OfType<RangeValidatorAttribute>().Any()))
                .ThenForEach(x => x.GetCustomAttributes(true).AsResult()
                    .Then(y => y.OfType<RangeValidatorAttribute>())
                    .Then(y => y.First())
                    .Then(y => y.Validate((IComparable) x.GetValue(this))))
                .ValidateForFail();
        }
        
        private Result<bool> ValidateStrings()
        {
            return GetType().AsResult()
                .Then(x => x.GetProperties())
                .Then(x => x.Where(y => y.GetCustomAttributes(true).OfType<StringValidatorAttribute>().Any()))
                .ThenForEach(x => x.GetCustomAttributes(true).AsResult()
                    .Then(y => y.OfType<StringValidatorAttribute>())
                    .Then(y => y.First())
                    .Then(y => y.Validate((string) x.GetValue(this))))
                .ValidateForFail();
        }

        private Result<bool> Validate()
        {
            return new Result<bool>().ThenCombineAndCheckAllForFail(_ => ValidateRanges(), _ => ValidateStrings());
        }

        private Color? GetColor()
        {
            var splittedColor = Color?.Split(' ').Select(int.Parse).ToList();
            return Color is null ? null : System.Drawing.Color.FromArgb(splittedColor[0], splittedColor[1], splittedColor[2]);
        }

        private Size GetSize()
        {
            var size = Size.Split(' ').Select(int.Parse).ToList();
            return new Size(size[0], size[1]);
        }

        public Result<Config> GetConfig()
        {
            return Validate().AsResult()
                .Then(_ => new Config())
                .ThenAction(x => x.SourceTextInterpretationMode = IsLiteraryText ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine)
                .ThenAction(x => x.TagCloudResultActions = Open ? TagCloudResultActions.SaveAndOpen : TagCloudResultActions.Save)
                .ThenAction(x => x.CustomIgnoreFilePath = IgnoreWordsFileName)
                .ThenAction(x => x.TextFilePath = TextFilePath)
                .ThenAction(x => x.Font = Font)
                .ThenAction(x => x.MinWordToStatisticLength = (byte)MinWordLengthToStatistic)
                .ThenAction(x => x.Density = (uint)Density)
                .ThenAction(x => x.WordCountToStatistic = (uint)WordCountToStatistic)
                .ThenAction(x => x.Color = GetColor())
                .ThenAction(x => x.SavePath = SavePath)
                .ThenAction(x => x.SaveFileName = SaveFileName)
                .ThenAction(x => x.Size = GetSize());
        }
    }
}