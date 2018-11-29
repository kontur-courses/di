using System;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
	public class CloudParametersParser
	{
		public static CloudParameters Parse(string[] input)
		{
			ICurve curve = null;
			var count = 0;
			var maxLengthRect = 0;

			for (var i = 0; i < input.Length; i++)
			{
				switch (input[i])
				{
					case "-count":
						int.TryParse(input[i + 1], out count);
						break;
					case "-maxLength":
						int.TryParse(input[i + 1], out maxLengthRect);
						break;
					case "-curve":
						curve = GetCurve(input, i);
						break;
				}
			}

			return new CloudParameters
			{
				Count = count,
				Curve = curve,
				MaxLengthRect = maxLengthRect
			};
		}

		private static ICurve GetCurve(string[] args, int position)
		{
			ICurve curve = null;
			switch (args[position + 1])
			{
				case "spiral":
					curve = new Spiral(0.2, Math.PI / 36);
					break;
				case "heart":
					curve = new Heart(0.2, Math.PI / 36);
					break;
				case "astroid":
					curve = new Astroid(0.2, Math.PI / 36);
					break;
			}

			return curve;
		}
	}
}