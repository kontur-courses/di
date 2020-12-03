using CloudContainer.ConfigCreators;
using MatthiWare.CommandLine;

namespace CloudContainer.ArgumentParser
{
    public class ArgumentParser : IArgumentParser
    {
        public Arguments Parse(string[] args)
        {
            var parser = new CommandLineParser<Arguments>();
            var arguments = parser.Parse(args);
            return parser.Parse(args).Result;
        }
    }
}