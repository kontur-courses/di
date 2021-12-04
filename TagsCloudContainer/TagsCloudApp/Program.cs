using CommandLine;

namespace TagsCloudApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Parser.Default.ParseArguments<RenderOptions>(args)
                .WithParsed(RenderOptions.HandleCommand);

            return 1;
        }
    }
}