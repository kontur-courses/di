namespace TagsCloud
{
    public class TagCloudRender
    {
        private readonly IFrequencyCollection collection;
        private readonly CoordinatesAtImage coordinatesAtImage;
        private readonly IGraphics graphics;
        private readonly ITagCloudLayouter layout;
        private readonly IBoringWordsCollection words;

        public TagCloudRender(ITagCloudLayouter layout, CoordinatesAtImage coordinatesAtImage,
            IBoringWordsCollection words,
            IFrequencyCollection collection, IGraphics graphics)
        {
            this.layout = layout;
            this.coordinatesAtImage = coordinatesAtImage;
            this.words = words;
            this.collection = collection;
            this.graphics = graphics;
        }

        public void Render()
        {
            var frequencyDictionary = collection.GetFrequencyCollection(words.DeleteBoringWords());
            var wordsToDraw = layout.GetLayout(frequencyDictionary);
            var coordinates = coordinatesAtImage.GetCoordinates(wordsToDraw);
            graphics.Save(coordinates);
        }
    }
}