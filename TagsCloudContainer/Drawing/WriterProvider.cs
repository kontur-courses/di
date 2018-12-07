namespace TagsCloudContainer.Drawing
{
    public class WriterProvider
    {
        public IWriter Writer { get; set; }

        public WriterProvider(IWriter writer)
        {
            Writer = writer;
        }
    }
}