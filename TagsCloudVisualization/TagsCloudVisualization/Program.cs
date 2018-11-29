using System;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
	public class CloudWordsForm
	{
		private static void Main(string[] args)
		{
			var parameters = CloudParametersParser.Parse(args);

			if (!parameters.IsCorrect())
				return;

			var cloud = new CircularCloudLayouter(parameters.Curve);
			var rectangles = GetData(cloud, parameters.Count, parameters.MaxLengthRect);
			var picture = RectangleTagsCloudVisualizer.GetPicture(rectangles, Color.Aqua);
			picture.Save($"{Application.StartupPath}\\CloudTags.png");
			Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
		}

		public static Rectangle[] GetData(CircularCloudLayouter cloud, int number, int maxLength)
		{
			var rnd = new Random();
			for (var i = 0; i < number; i++)
				cloud.PutNextRectangle(new Size(rnd.Next(5, maxLength), rnd.Next(5, 30)));

			return cloud.GetRectangles();
		}
	}
}