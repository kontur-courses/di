using System.IO;

namespace TagsCloudContainer.Common
{
    public class Serializer : ISerializer
    {
        public string Deserialize(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
                return new StreamReader(ms).ReadToEnd();
        }

        public byte[] Serialize(string str)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    foreach (var c in str)
                        sw.Write(c);
                    return ms.ToArray();
                }
            }
        }
    }
}