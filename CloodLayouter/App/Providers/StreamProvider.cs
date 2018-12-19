using System.IO;
using System.Runtime.InteropServices.ComTypes;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class StreamProvider : IProvider<StreamReader>
    {      
        public StreamProvider(string filename)
        {
            stream = new StreamReader(filename);
        }

        private StreamReader stream;
        
        public StreamReader Get()
        {
            return stream;
        }
    }
}