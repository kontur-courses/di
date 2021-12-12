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
            using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
            {
                foreach (var entry in zip.Entries)
                {
                    if (entry.Name == "document.xml")
                    {
                        NameTable nt = new NameTable();
                        XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
                        nsManager.AddNamespace("w", wordmlNamespace);

                        var SB = new StringBuilder();
                        var xml = new XmlDocument(nt);
                        xml.Load(entry.Open());
                        var nodes = xml.SelectNodes("//w:p", nsManager);
                        foreach (XmlNode node in nodes)
                        {
                            SB.Append(node.InnerText + "\r");
                        }
                        var text = SB.ToString();
                        return text;
                    }
                }
            }
            throw new Exception();
        }
    }
}
