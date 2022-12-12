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

            var container = ContainerBuilder.GetNewTagCloudServices(width, height);

            var client = container.GetService<IClient>();
            client.StartClient();

            var tagCloud = container.GetService<ITagCloud>();

            tagCloud.PrintTagCloud(client.TextFilePath,
                client.PicFilePath,
                client.PicFileExtension);
        }
    }
}
