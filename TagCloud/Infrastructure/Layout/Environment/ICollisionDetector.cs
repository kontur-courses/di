namespace TagCloud.Infrastructure.Layout.Environment
{
    public interface ICollisionDetector<in T>
    {
        public bool IsColliding(T element);
    }
}