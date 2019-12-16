using System.Collections.Generic;
using TagCloudPainter;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class DrawTagCloud : IConsoleCommand
    {
        private readonly IFileTextRider fileTextRider;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly ITagCloudPainter tagCloudPainter;
        
        public DrawTagCloud(IFileTextRider fileTextRider, ITextAnalyzer textAnalyzer, ITagCloudPainter tagCloudPainter)
        {
            this.fileTextRider = fileTextRider;
            this.textAnalyzer = textAnalyzer;
            this.tagCloudPainter = tagCloudPainter;
        }
        
        public string Name => "DrawTC";
        public string Description => "Рисует облако со стандартным присетом и сохраняет в текущей директории";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            TagCloudCreator.Create(fileTextRider, textAnalyzer, tagCloudPainter);
        }
        public List<string> ArgsName => new List<string>();
    }
}