namespace TagsCloud2;

public interface IManager
{
    public void Manage(IReader reader,
        ILemmatizer lemmatizer,
        IFrequencyCompiler frequencyCompiler,
        IImageSaver imageSaver, 
        ITagsCloudMaker tagsCloudMaker);
}