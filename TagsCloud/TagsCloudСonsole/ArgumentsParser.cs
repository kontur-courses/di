namespace TagsCloudConsole
{
     public class ArgumentsParser
    {

        private const string usage = @"Tag Cloud Generator 2.0.
    Usage:
      TagsCloudConsole.exe --file FILE [--image_name IMAGENAME] [--extention EXTENTION] [--font FONT] [--colors COLORS]
                                [--filter FILTER] [--exclude PARTS_OF_SPEECH] [--print_only PART_OF_SPEECH] 
                                [--image_width WIDTH]  [--image_height HEIGHT] [--radius radius] --x_coord COORD --y_coord COORD --text_size SIZE
                                
    Filters:
      POS - Part Of Speech filter. Flags:  --exclude PARTS_OF_SPEECH  In quotes list parts of speech to exclude[default: '']    .
                                           -po --print_only PART_OF_SPEECH  Will create cloud only with indicated part of speech[default: '']. 
    Options:
      -h --help  Show this screen.
      -f --file FILE  File with text for cloud.
      -im --image_name IMAGENAME  Final image name[default: tagCloudVisualization].
      --font FONT  Font of words that will be drawn in picture[default: Consolas].
      -c --colors COLORS  Word's colors[default: black].
      -e --extention EXTENTION  Final image extention[default: .png].
      -w --image_width WIDTH  Final image width[default: 0].
      -h --image_height HEIGHT  Final image height[default: 0].
      -r --radius radius  Tag cloud radious[default: 0].
      -x --x_coord COORD  X coordinate of tag cloud center.
      -y --y_coord COORD  Y coordinate of tag cloud center.
      --filter FILTER  Filter for tag cloud[default: POS]
      --text_size SIZE
      --exclude PARTS_OF_SPEECH  In quotes list parts of speech to exclude[default: ''].
      -po --print_only PART_OF_SPEECH  Will create cloud only with indicated part of speech[default: '']. 
    ";

        /*
        public CloudSettings CreateCloudSettings(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var point = new Point(int.Parse(arguments["--x_coord"].Value.ToString()), int.Parse(arguments["--y_coord"].Value.ToString()));
            var radius = int.Parse(arguments["--radius"].Value.ToString());
            radius = radius == 0 ? int.MaxValue : radius;
            return new CloudSettings(point, radius);
        }

        public ImageSettings CreateImageSettings(string[] args, ITextRenderer textRenderer)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var width = int.Parse(arguments["--image_width"].Value.ToString());
            var height = int.Parse(arguments["--image_height"].Value.ToString());
            var size = new Size(width, height);
            var imageName = arguments["--image_name"].Value.ToString();
            var extention = arguments["--extention"].Value.ToString();
            var textSize = int.Parse(arguments["--text_size"].Value.ToString());
            var font = arguments["--font"].Value.ToString();
            var colors = arguments["--colors"].Value.ToString();
            return new ImageSettings(size, imageName, extention, textSize, font, colors, textRenderer);
        }

        public TextSettings CreateTextSettings(string[] args, IMorphAnalyzer analyzer)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            var path = arguments["--file"].Value.ToString();
            var filter = arguments["--filter"].Value.ToString();
            var partsOfSpeech = arguments["--exclude"].Value.ToString();
            var partOfSpeech = arguments["--print_only"].Value.ToString();
            return new TextSettings(path, filter, partsOfSpeech, partOfSpeech, analyzer);
        }*/
    }
}