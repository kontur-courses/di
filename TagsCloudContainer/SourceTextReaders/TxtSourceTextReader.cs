using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace TagsCloudContainer.SourceTextReaders
{
    public class TxtSourceTextReader : ISourceTextReader
    {
        public string[] ReadText(string resourceName)
        {
            //            var encoding = Encoding.GetEncoding(Encoding.Default);
            //            using (var sr = new StreamReader(filePath, Encoding.Unicode))
            //            {
            //                var s = 
            //                return sr.ReadToEnd().Split('\n');
            //            }
            //                return System.IO.File.ReadAllLines(filePath);

            var assembly = Assembly.GetExecutingAssembly(); 

//            resourceName = FormatResourceName(assembly, resourceName);
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {

                if (resourceStream == null)
                {
                    var x = assembly.GetManifestResourceNames();
                    return null;

                }

                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd().Split('\n');
                }
            }
        }

        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                       .Replace("\\", ".")
                       .Replace("/", ".");
        }
    }
}
