using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloud.Interfaces;
using TagsCloud.TextWorkers;
using System;


namespace TagsCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            var width = 1024;
            var height = 720;

            var consoleClient = new ConsoleClient(@"..\..\..\..\");
            consoleClient.StartClient();

            var tagCloud = ContainerBuilder
                .GetNewTagCloudServices(width, height)
                .GetService<TagCloud>();

            tagCloud.PrintTagCloud(consoleClient.TextFilePath,
                consoleClient.PicFilePath,
                consoleClient.PicFileExtension);
        }
    }
}
