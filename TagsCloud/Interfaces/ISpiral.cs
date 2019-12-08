using System.Drawing;

namespace TagsCloud.Interfaces
{
	public interface ISpiral
	{
		Point GetNextPoint();
		void ResetState();
	}
}