namespace TagsCloud.Interfaces
{
	public interface IMenuAction
	{
		string Category { get; }
		string Name { get; }
		string Description { get; }
		void Perform();
	}
}