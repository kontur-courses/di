namespace TagsCloudContainer.Common
{
    public class TagWriter
    {
        private readonly IBlobStorage storage;
        private readonly ISerializer serializer;

        public TagWriter(IBlobStorage storage, ISerializer serializer)
        {
            this.storage = storage;
            this.serializer = serializer;
        }

        public void Write(string name, string text)
        {
            storage.Set(name, serializer.Serialize(text));
        }
    }
}