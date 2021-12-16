using System;
using CommandLine;

namespace TagCloudUsageSample
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var options = ClTextOptions.GetDefaultOption();
             if (!options.Validate(out var message))
             {
                 Console.WriteLine(message);
                 return;
             }

             options.CreateTags(out var firstFileName);
             if (firstFileName is not null && options.Open)
                 System.Diagnostics.Process.Start(firstFileName);
            // Parser.Default
            //     .ParseArguments<ClTextOptions>(args)
            //     .WithParsed(options =>
            //     {
            //         if (!options.Validate(out var message))
            //         {
            //             Console.WriteLine(message);
            //             return;
            //         }
            //
            //         options.CreateTags(out var firstFileName);
            //         if (firstFileName is not null && options.Open)
            //             System.Diagnostics.Process.Start(firstFileName);
            //     });
        }
    }
}