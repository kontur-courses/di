using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGraphicalUserInterface
{
    public interface IDependency<in T>
    {
        void SetDependency(T dependency);
    }
}
