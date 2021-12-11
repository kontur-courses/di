using System;
using System.Text;
using TagsCloudVisualization;

namespace TagsCloudClients
{
    public class ConsoleClient
    {
        private readonly ITagCloudCreator cloudCreator;

        public ConsoleClient(ITagCloudCreator cloudCreator)
        {
            this.cloudCreator = cloudCreator;
        }

        public void Run()
        {
            
        }
    }
}