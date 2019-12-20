using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Providers.WordSource.Readers;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.WordSource
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

        public Result<List<string>> GetObjectSource(ReaderSettings settings)
        {
            var words = GetWords(settings.PathToText);
            var badWords = GetWords(settings.BadWordsPath);
            if (!words.IsSuccess || !badWords.IsSuccess)
                return Result.Fail<List<string>>(
                    $"Can't read file on path {(words.IsSuccess ? settings.BadWordsPath : settings.PathToText)}");
            settings.BadWords = badWords.Value;
            var changed = ChangeWords(settings, words.Value);
            var selected = SelectWords(settings, changed).ToList();
            return selected.AsResult();
        }

        private Result<IEnumerable<string>> GetWords(string path)
        {
            var lines = textReaderFactory.GetReader(path).ReadLines(path);
            if (!lines.IsSuccess)
                return Result.Fail<IEnumerable<string>>(lines.Error);
            var words = textSplitter.SplitByPunctuation(lines.Value);

            return words.AsResult();
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