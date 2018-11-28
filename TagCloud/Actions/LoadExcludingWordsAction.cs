using System.IO;
using System.Windows.Forms;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.Words;

namespace TagCloud.Actions

{
    class LoadExcludingWordsAction : IUiAction
    {
        private readonly ExcludingWords words;
        public LoadExcludingWordsAction(ExcludingWords words)
        {
            this.words = words;
        }
        public string Category { get; } = "File";
        public string Name { get; } = "Excluding Words";
        public string Description { get; } = "Load words to exclude from Tag Cloud";
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
                    words.Load(splittedWords);
                }
            }
            
        }
    }
} 