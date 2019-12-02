using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Actions
{
    public class SaveImageAction : IAction
    {
        private ICloudVisualization Visualization { get;}

        public string CommandName => "- SaveImage";

        public SaveImageAction(ICloudVisualization visualization)
        {
            this.Visualization = visualization;
        }
        public void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
