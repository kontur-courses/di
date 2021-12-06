namespace TagsCloudContainer.Common
{
    public interface ISerializer
    {
        string Deserialize(byte[] bytes);
        byte[] Serialize(string obj);
    }
}