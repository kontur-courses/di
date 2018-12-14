using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface IApplicationRunner
    {
        void Run(IApplication application, string[] args);
    }
}
