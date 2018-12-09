using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloudCreation;

namespace TagCloudApp
{
    internal abstract class UserInterface
    {
        protected readonly TagCloudCreator Creator;
        protected readonly Dictionary<string, List<ITextReader>> Readers;

        protected UserInterface(TagCloudCreator creator, IEnumerable<ITextReader> readers)
        {
            Creator = creator;
            Readers = readers.GroupBy(r => r.Extension)
                             .ToDictionary(g => g.Key, g => g.ToList());
        }

        public abstract void Run(string[] startupArgs);

        protected bool TryRead(string path, out IEnumerable<string> words)
        {
            words = null;
            var extension = Path.GetExtension(path);
            if (extension == null)
                return false;
            if (!Readers.TryGetValue(extension, out var reader))
                return false;
            foreach (var textReader in reader)
            {
                if (textReader.TryReadWords(path, out words))
                    return true;
            }
            return false;
        }
    }
}
