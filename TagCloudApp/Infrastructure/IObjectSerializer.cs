namespace TagCloudApp.Infrastructure;

public interface IObjectSerializer
{
    T? Deserialize<T>(byte[] bytes);
    byte[] Serialize<T>(T obj);
}