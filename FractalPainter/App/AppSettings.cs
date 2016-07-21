using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FractalPainting.App
{
	public interface IImageDirectorySettings
	{
		string ImagePath { get; }
	}

	public class AppSettings : IImageDirectorySettings
	{
		public string ImagePath { get; set; }
		public ImageSettings ImageSettings { get; set; }
		public FormWindowState MainWindowState { get; set; }
	}
}