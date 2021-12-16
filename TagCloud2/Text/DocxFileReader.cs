using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TagCloud2.Text
{
    public class DocxFileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            using (var file = File.OpenRead(path))
            {
                using var zip = new ZipArchive(file, ZipArchiveMode.Read);
                var entry = zip.Entries.Where(x => x.Name == "document.xml").First();
                NameTable nt = new NameTable();
                XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
                nsManager.AddNamespace("w", wordmlNamespace);
                var SB = new StringBuilder();
                var xml = new XmlDocument(nt);
                using var doc = entry.Open();
                xml.Load(doc);
                var nodes = xml.SelectNodes("//w:p", nsManager);
                foreach (XmlNode node in nodes)
                {
                    SB.Append(node.InnerText + "\r");
                }

                return SB.ToString();
            }

            throw new ArgumentException("this is not docx document!");
        }
    }
}
