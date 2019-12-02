using System;
using System.Drawing;
using TagsCloudGenerator.Executors;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Generators
{
    public class DefaultTagsCloudGenerator : ITagsCloudGenerator
    {
        private readonly IWordsParser parser;
        private readonly IExecutor<string[], string[]> convertersExecutor;
        private readonly IExecutor<string[], string[]> filtersExecutor;
        private readonly IWordsLayouter layouter;
        private readonly IPainterAndSaver painterAndSaver;

        public DefaultTagsCloudGenerator(
            IWordsParser parser,
            IWordsConverter[] converters,
            IWordsFilter[] filters,
            IWordsLayouter layouter,
            IPainterAndSaver painterAndSaver)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            convertersExecutor = converters != null ? new StrArrToStrArrPriorityExecutor(converters) : throw new ArgumentNullException(nameof(converters));
            filtersExecutor = filters != null ? new StrArrToStrArrPriorityExecutor(filters) : throw new ArgumentNullException(nameof(filters));
            this.layouter = layouter ?? throw new ArgumentNullException(nameof(layouter));
            this.painterAndSaver = painterAndSaver ?? throw new ArgumentNullException(nameof(painterAndSaver));
        }

        public bool TryGenerate(string fromFile, string font, Size imageSize, string pathToSave)
        {
            var parsedWords = parser.ParseFromFile(fromFile);
            var converteredWords = convertersExecutor.Execute(parsedWords);
            var filteredWords = filtersExecutor.Execute(converteredWords);
            var layoutedWords = layouter.ArrangeWords(filteredWords, font);
            return painterAndSaver.TryPaintAndSave(layoutedWords, imageSize, pathToSave);
        }
    }
}