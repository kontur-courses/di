using System.IO;
using System.Windows.Forms;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.Words;

namespace TagCloud.Actions

{
    class LoadExcludingWordsAction : IUiAction
    {
        private readonly ExcludingWordsRepository wordsRepository;
        public LoadExcludingWordsAction(ExcludingWordsRepository wordsRepository)
        {
            this.wordsRepository = wordsRepository;
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
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    var splittedWords = WordAnalyzer.SplitWords(fileContent);
                    wordsRepository.Load(splittedWords);
                }
            }
            
        }
    }
} 