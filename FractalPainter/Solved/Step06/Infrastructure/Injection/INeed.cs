namespace FractalPainting.Solved.Step06.Infrastructure.Injection
{
    public interface INeed<in T>
    {
        void SetDependency(T dependency);
    }
}