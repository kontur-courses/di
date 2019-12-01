namespace FractalPainting.Solved.Step01.Infrastructure.Injection
{
    public interface INeed<in T>
    {
        void SetDependency(T dependency);
    }
}