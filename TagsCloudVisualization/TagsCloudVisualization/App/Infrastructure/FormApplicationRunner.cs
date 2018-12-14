using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class FormApplicationRunner : IApplicationRunner
    {
        public void Run(IApplication application, string[] args)
        {
            Application.Run(application as Form);
        }
    }
}
