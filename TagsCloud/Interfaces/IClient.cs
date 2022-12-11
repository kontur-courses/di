namespace TagsCloud.Interfaces
{
    public interface IClient
    {
        public string TextFilePath { get; }
        public string PicFilePath { get; }
        public string PicFileExtension { get; }

        public void StartClient();
    }
}
