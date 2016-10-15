namespace FractalPainting.Infrastructure
{
	public interface INeed<in T>
	{
		void SetDependency(T dependency);
	}
}