using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TagsCloudContainer
{
    class ConsoleUI : IUserInterface
    {
        private readonly ICloudVisualizer visualizer;

        public ConsoleUI(ICloudVisualizer visualizer)
        {
            this.visualizer = visualizer;
        }

        public void VisualizeCloud()
        {
            visualizer.VisualizeCloud();
        }
    }
}
