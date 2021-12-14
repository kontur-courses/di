namespace TagCloud.visual
{
    public interface ISaver<in T>
    {
        void Save(T obj);
    }
}