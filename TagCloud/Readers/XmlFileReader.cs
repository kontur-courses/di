using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TagCloud.Readers
{
    public class XmlFileReader : IFileReader
    {
        public string[] ReadFile(string filename)
        {
            var doc = new XmlDocument();
            var text = new List<string>();
            doc.Load(filename);
            if (doc.DocumentElement == null)
                throw new InvalidOperationException("Invalid content format");

            if (doc.DocumentElement.Name != "words")
                throw new InvalidOperationException("Xml root should called \"words\"");

            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
                text.Add(node.InnerText);
            return text.ToArray();
        }
    }
}
