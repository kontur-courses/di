using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;
using TagsCloudVisualization.WordSource.Readers;

namespace TagsCloudVisualization.WordSource
{
    internal class WordSourceProvider : IWordsProvider
    {
        private readonly IChangerFactory changerFactory;
        private readonly ISelectorFactory selectorFactory;
        private readonly TextReaderFactory textReaderFactory;
        private readonly IWordReader textSplitter;

        public WordSourceProvider(TextReaderFactory textReaderFactory, IWordReader textSplitter,
            IChangerFactory changerFactory,
            ISelectorFactory selectorFactory)
        {
            this.textReaderFactory = textReaderFactory;
            this.changerFactory = changerFactory;
            this.selectorFactory = selectorFactory;
            this.textSplitter = textSplitter;
        }

        public IEnumerable<string> GetObjectSource(ReaderSettings settings)
        {
            var words = GetWords(settings.PathToText);
            settings.BadWords = GetWords(settings.BadWordsPath);
            var changed = ChangeWords(settings, words);
            var selected = SelectWords(settings, changed);
            return selected;
        }

        private IEnumerable<string> GetWords(string path)
        {
            var lines = textReaderFactory.GetReader(path).ReadLines(path);
            var words = textSplitter.SplitByPunctuation(lines);
            return words;
        }

        private IEnumerable<string> ChangeWords(ReaderSettings settings, IEnumerable<string> wordSource)
        {
            string changed;
            return wordSource.Select(w =>
            {
                changed = w;
                foreach (var changer in changerFactory.GetChangers(settings))
                {
                    changed = changer.Change(changed);
                }

                return changed;
            });
        }

        private IEnumerable<string> SelectWords(ReaderSettings settings, IEnumerable<string> wordSource)
        {
            return wordSource.Where(w =>
            {
                var isSelected = true;
                foreach (var changer in selectorFactory.GetSelectors(settings))
                {
                    isSelected = isSelected && changer.Select(w);
                }

                return isSelected;
            });
        }
    }
}