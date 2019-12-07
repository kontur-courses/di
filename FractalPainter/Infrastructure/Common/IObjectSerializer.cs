namespace FractalPainting.Infrastructure.Common
{
    public interface IObjectSerializer
    {
        T Deserialize<T>(byte[] bytes);
        byte[] Serialize<T>(T obj);
    }
}