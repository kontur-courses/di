namespace FractalPainting.Infrastructure
{
	public interface INeed<T>
	{
		void SetDependency(T dependency);
	}
}