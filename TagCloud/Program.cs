using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;

namespace TagCloud
{
    public class Program
    { 
        static void Main(string[] args)
        {
            var appConfig = new AppConfigProvider(args).GetAppConfig();

            var container = ContainerConfig.Configure(appConfig);

            var app = container.Resolve<IApp>();

            app.Run(appConfig);
        }

        private static void CreateTestText()
        {
            var lines = File.ReadAllLines("TestWords.txt");

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                var chars = tokens[0].ToCharArray();

                chars[0] = char.ToUpper(chars[0]);

                var word = new string(chars);

                var frequency = Convert.ToInt32(Math.Round(double.Parse(tokens[1]) / 10));

                dict.Add(word, frequency);
            }

            var sb = new StringBuilder();

            foreach (var pair in dict)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    sb.AppendLine(pair.Key);
                }
            }

            File.WriteAllText("TestText.txt", sb.ToString());
        }
    }
}

 