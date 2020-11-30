using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.Clients
{
    internal interface IClient
    {
        public void Visualizate(string worsPath, string picturePath);
    }
}
