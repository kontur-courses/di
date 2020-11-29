namespace FractalPainting.Solved.Step03.Infrastructure.Injection
{
    public interface INeed<in T>
    {
        void SetDependency(T dependency);
    }
}