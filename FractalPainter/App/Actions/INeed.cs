namespace FractalPainting.App.Actions
{
	public interface INeed<T>
	{
		void SetDependency(T dependency);
	}
}