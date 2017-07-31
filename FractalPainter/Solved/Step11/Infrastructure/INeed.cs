namespace FractalPainting.Solved.Step11.Infrastructure
{
	public interface INeed<in T>
	{
		void SetDependency(T dependency);
	}
}