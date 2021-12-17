using CommandLine;
using System;
using TagsCloudContainer.Client;

namespace CLI
{
    public class Client : IClient
    {
        private readonly string[] args;
        public UserConfig UserConfig { get; }

        public Client(string[] args)
        {
            this.args = args;
            UserConfig = GetUserConfig();
        }

        private UserConfig GetUserConfig()
        {
            var userConfig = new UserConfig();
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options => userConfig.GetConfig(options))
                .WithNotParsed(errs => throw new Exception(
                    $"Failed with errors:\n{string.Join("\n", errs)}"));

            return userConfig;
        }
    }
}
