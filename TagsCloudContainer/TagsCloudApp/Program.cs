using System;
using CommandLine;

namespace TagsCloudApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<RenderOptions>(args)
                .WithParsed(options =>
                {
                    try
                    {
                        new RenderCommand(options).Render();
                    }
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
        }
    }
}