using System.Drawing;

namespace TagsCloud.Distributors;

public interface IDistributor
{
     Point GetNextPosition();
}