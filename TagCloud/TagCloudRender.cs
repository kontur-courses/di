namespace TagsCloud
{
    public class TagCloudRender
    {
        private readonly FrequencyDictionary dictionary;
        private readonly CoordinatesAtImage coordinatesAtImage;
        private readonly CreateLayout layout;
        private readonly IWordCollection words;

        public TagCloudRender(CreateLayout layout, CoordinatesAtImage coordinatesAtImage, IWordCollection words,
            FrequencyDictionary dictionary)
        {
            this.layout = layout;
            this.coordinatesAtImage = coordinatesAtImage;
            this.words = words;
            this.dictionary = dictionary;
        }


        public void Render()
        {
            var frequencyDictionary = dictionary.GetFrequencyDictionary(words.GetWords());
            var wordsToDraw = layout.GetLayout(frequencyDictionary);
            coordinatesAtImage.GetCoordinates(wordsToDraw);
        }
    }
}