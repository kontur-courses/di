namespace FractalPainting.Solved.Step04.Infrastructure.Injection
{
    public interface INeed<in T>
    {
        void SetDependency(T dependency);
    }
}