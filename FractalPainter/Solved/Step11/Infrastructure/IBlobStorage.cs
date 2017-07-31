namespace FractalPainting.Solved.Step11.Infrastructure
{
	public interface IBlobStorage
	{
		byte[] Get(string name);
		void Set(string name, byte[] content);
	}
}