using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.App
{
    public class ConsoleApplicationRunner : IApplicationRunner
    {
        public void Run(IApplication application, string[] args)
        {
            var app = application as ConsoleApplication;
            app.GenerateImage(args);
        }
    }
}
