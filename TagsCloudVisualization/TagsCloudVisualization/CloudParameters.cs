using System;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
	public class CloudParameters
	{
		public int MaxLengthRect { get; set; }
		public int Count { get; set; }
		public ICurve Curve { get; set; }

		public bool IsCorrect()
		{
			if (Curve == null)
			{
				Console.WriteLine("Error in the name of the curve. You need to choose one of them: spiral | heart | astroid");
				return false;
			}

			if (Count <= 0)
			{
				Console.WriteLine("Count must be positive");
				return false;
			}

			if (MaxLengthRect <= 0)
			{
				Console.WriteLine("MaxLength must be positive");
				return false;
			}

			return true;
		}
	}
}