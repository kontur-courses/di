namespace TagsCloudForm.Common
{
    public interface IBlobStorage
    {
        byte[] Get(string name);
        void Set(string name, byte[] content);
    }
}