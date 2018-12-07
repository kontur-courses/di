namespace TagsCloud
{
    public class TagCloudRender
    {
        private readonly FrequencyDictionary dictionary;
        private readonly Graphics graphics;
        private readonly CreateLayout layout;
        private readonly IWordCollection words;

        public TagCloudRender(CreateLayout layout, Graphics graphics, IWordCollection words,
            FrequencyDictionary dictionary)
        {
            this.layout = layout;
            this.graphics = graphics;
            this.words = words;
            this.dictionary = dictionary;
        }


        public void Render()
        {
            var frequencyDictionary = dictionary.GetFrequencyDictionary(words.GetWords());
            var wordsToDraw = layout.GetLayout(frequencyDictionary);
            graphics.SaveMap(wordsToDraw);
        }
    }
}