using System;
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
            var ret = new UserConfig();
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options =>
                {
                    ret = new UserConfig(options);
                }).WithNotParsed(errs =>
                {
                    Console.WriteLine("Failed with errors:\n{0}",
                        string.Join("\n", errs));
                });

            return ret;
        }
    }
}
