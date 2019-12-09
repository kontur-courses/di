using TagsCloudGenerator.Executors;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Generators
{
    public class TagsCloudGenerator : IGenerator
    {
        private readonly IFactory<IWordsParser> parsersFactory;
        private readonly IFactory<IWordsConverter> convertersFactory;
        private readonly IFactory<IWordsFilter> filtersFactory;
        private readonly IFactory<IWordsLayouter> layoutersFactory;
        private readonly IPainterAndSaver painterAndSaver;

        public TagsCloudGenerator(
            IFactory<IWordsParser> parsersFactory,
            IFactory<IWordsConverter> convertersFactory,
            IFactory<IWordsFilter> filtersFactory,
            IFactory<IWordsLayouter> layoutersFactory,
            IPainterAndSaver painterAndSaver)
        {
            this.parsersFactory = parsersFactory;
            this.convertersFactory = convertersFactory;
            this.filtersFactory = filtersFactory;
            this.layoutersFactory = layoutersFactory;
            this.painterAndSaver = painterAndSaver;
        }

        public bool TryGenerate(string readFromPath, string saveToPath)
        {
            var parsedWords = parsersFactory.CreateSingle().ParseFromFile(readFromPath);
            var converteredWords = new PriorityExecutor<string[]>(convertersFactory.CreateArray())
                .Execute(parsedWords);
            var filteredWords = new PriorityExecutor<string[]>(filtersFactory.CreateArray())
                .Execute(converteredWords);
            var layoutedWords = layoutersFactory.CreateSingle().ArrangeWords(filteredWords);
            return painterAndSaver.TryPaintAndSave(layoutedWords, saveToPath);
        }
    }
}