namespace FractalPainting.Solved.Step11.Modified
{
    public interface IUiAction
	{
        MenuCategory Category { get; }
		string Name { get; }
		string Description { get; }
		void Perform();
	}
}