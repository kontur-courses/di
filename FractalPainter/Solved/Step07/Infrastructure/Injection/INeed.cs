namespace FractalPainting.Solved.Step07.Infrastructure.Injection
{
    public interface INeed<in T>
    {
        void SetDependency(T dependency);
    }
}