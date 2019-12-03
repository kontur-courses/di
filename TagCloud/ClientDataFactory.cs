using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.IServices;

namespace TagCloud
{
    class ClientDataFactory : IClientDataFactory
    {
        public Settings settings { get; set; }
        public ClientData CreateData()
        {
            return new ClientData(settings.Width,settings.Height);
        }
    }
}
