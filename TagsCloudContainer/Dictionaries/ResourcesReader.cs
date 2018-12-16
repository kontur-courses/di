using System.IO;
using System.Reflection;

namespace TagsCloudContainer.Dictionaries
{
    public class ResourcesReader : IReaderResources
    {
        private readonly Assembly assembly = Assembly.GetExecutingAssembly();

        public byte[] Read(string fileName)
        {
            using (var stream = assembly.GetManifestResourceStream(fileName))
                return ReadStream(stream);
        }

        private byte[] ReadStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}