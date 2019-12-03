using System.Drawing;
using DocoptNet;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Parsers
{
    class ArgumentParser: IArgumentParser
    {

        private const string usage = @"TagCloud.
    Usage:
      TagsCloudVisualization.exe --file FILE [--image_name IMAGENAME] [--extention EXTENTION] [--filter FILTER] --image_width WIDTH 
                                 --image_height HEIGHT --radius radius --x_coord COORD --y_coord COORD --text_size SIZE

    Options:
      --help  Show this screen.
      -f --file FILE  File with text for cloud.
      -im --image_name IMAGENAME  Final image name[default: tagCloudVisualization].
      -e --extention EXTENTION  Final image extention[default: .png].
      -w --image_width WIDTH  Final image width.
      -h --image_height HEIGHT  Final image height.
      -r --radius radius  Tag cloud radious.
      -x --x_coord COORD  X coordinate of tag cloud center.
      -y --y_coord COORD  Y coordinate of tag cloud center.
      --filter FILTER  Filter for tag cloud[default: POS]
      --text_size SIZE
    ";

        public CloudSettings CreateCloudSettings(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var point = new Point(int.Parse(arguments["--x_coord"].Value.ToString()), int.Parse(arguments["--y_coord"].Value.ToString()));
            var radius = int.Parse(arguments["--radius"].Value.ToString());
            return new CloudSettings(point, radius);
        }

        public ImageSettings CreateImageSettings(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var size = new Size(int.Parse(arguments["--image_width"].Value.ToString()), int.Parse(arguments["--image_height"].Value.ToString()));
            var imageName = arguments["--image_name"].Value.ToString();
            var extention = arguments["--extention"].Value.ToString();
            var textSize = int.Parse(arguments["--text_size"].Value.ToString());
            return new ImageSettings(size,  imageName, extention, textSize);
        }

        public TextSettings CreateTextSettings(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var path = arguments["--file"].Value.ToString();
            var filter = arguments["--filter"].Value.ToString();
            return new TextSettings(path, filter);
        }
    }
}
