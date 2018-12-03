using System;
using System.IO;
using System.Windows.Forms;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.Words;

namespace TagCloud.Actions
{
    public class LoadWordsAction : IUiAction
    {
        private readonly IRepository wordsRepository;
        private IWordAnalyzer wordAnalyzer;
        private IReader reader;

        public LoadWordsAction(IRepository wordsRepository, IWordAnalyzer wordAnalyzer, IReader reader)
        {
            this.wordsRepository = wordsRepository;
            this.wordAnalyzer = wordAnalyzer;
            this.reader = reader;
        }

        public string Category => "File";
        public string Name => "Tag Words";
        public string Description => "Load file with Tag WordsRepository";

        public void Perform()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                var fileContent = reader.Read(openFileDialog.FileName);
                var splittedWords = wordAnalyzer.SplitWords(fileContent);
                wordsRepository.Load(splittedWords);
            }

        }
    }



}