namespace TagsCloud.ClientGUI.Infrastructure
{
    public interface IBlobStorage
    {
        byte[] Get(string name);
        void Set(string name, byte[] content);
    }
}