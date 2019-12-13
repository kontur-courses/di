using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;
using TagsCloudVisualization.WordSource.Readers;

namespace TagsCloudVisualization.WordSource
{
    internal class WordSourceProvider:IObjectProvider<string>
    {
        private readonly TextReaderFactory textReaderFactory;
        private readonly IChangerFactory<string> changerFactory;
        private readonly ISelectorFactory<string> selectorFactory;
        private readonly IObjectReader<string> textSplitter;

        public WordSourceProvider(TextReaderFactory textReaderFactory, IObjectReader<string> textSplitter,
            IChangerFactory<string> changerFactory,
            ISelectorFactory<string> selectorFactory)
        {
            this.textReaderFactory = textReaderFactory;
            this.changerFactory = changerFactory;
            this.selectorFactory = selectorFactory;
            this.textSplitter = textSplitter;
        }

        public IEnumerable<string> GetObjectSource(ReaderSettings settings)
        {
            var lines = textReaderFactory.GetReader(settings.Path).ReadLines(settings.Path);
            var words = textSplitter.SplitByPunctuation(lines);
            var changed = ChangeWords(settings, words);
            var selected = SelectWords(settings, changed);
            return selected;
        }

        private IEnumerable<string> ChangeWords(ReaderSettings settings, IEnumerable<string> wordSource)
        {
            var changed = "";
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