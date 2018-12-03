using System.IO;
using System.Windows.Forms;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.Words;

namespace TagCloud.Actions

{
    class LoadExcludingWordsAction : IUiAction
    {
        private readonly IExcludingRepository wordsRepository;
        private IWordAnalyzer analyzer;
        private IReader reader;

        public LoadExcludingWordsAction(IExcludingRepository wordsRepository, IWordAnalyzer analyzer, IReader reader)
        {
            this.wordsRepository = wordsRepository;
            this.analyzer = analyzer;
            this.reader = reader;
        }
        public string Category { get; } = "File";
        public string Name { get; } = "Excluding Words";
        public string Description { get; } = "Load wordsRepository to exclude from Tag Cloud";
        public void Perform()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                var fileContent = reader.Read(openFileDialog.FileName);
                var splittedWords = analyzer.SplitWords(fileContent);
                wordsRepository.Load(splittedWords);
            }
            
        }
    }
} 