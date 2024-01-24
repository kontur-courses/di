using CommandLine;

namespace TagCloudDi
{
    abstract class MainClass
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<ArgumentOptions>(args).Value;
            
        }
    }
}