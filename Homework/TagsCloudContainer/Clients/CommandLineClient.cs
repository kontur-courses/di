using System;
using System.Linq;
using CommandLine;

namespace TagsCloudContainer.Clients
{
    public class CommandLineClient : IClient
    {
        private string[] args;
        public UserConfig UserConfig { get; }

        public CommandLineClient(string[] args)
        {
            this.args = args;
            UserConfig = GetUserConfig();
        }

        private UserConfig GetUserConfig()
        {
            var userConfig = new UserConfig();
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options => userConfig = new UserConfig(options))
                .WithNotParsed(errs => throw new Exception(
                    $"Failed with errors:\n{string.Join("\n", errs)}"));

            return userConfig;
        }
    }
}
