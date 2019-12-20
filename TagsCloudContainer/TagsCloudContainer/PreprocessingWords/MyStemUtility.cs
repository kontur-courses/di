using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TagsCloudContainer.PreprocessingWords
{
    public class MyStemUtility : IPreprocessingWords
    {
        private readonly string pathMyStemUtility;
        private readonly string flags;
        private readonly ICreateProcess createProcess;

        public IEnumerable<string> Preprocessing(IEnumerable<string> strings)
        {
            var pathTempFile = Path.GetTempFileName();
            try
            {
                using (var sw = File.CreateText(pathTempFile))
                    foreach (var str in strings)
                        sw.WriteLine(str);

                return createProcess
                    .GetResult(pathMyStemUtility, flags + " " + pathTempFile)
                    .Select(s => JsonConvert.DeserializeObject<MyStemOutput>(s).GetPrimaryFormOfNouns())
                    .Where(s => s != null);
                ;
            }
            finally
            {
                File.Delete(pathTempFile);
            }
        }

        public MyStemUtility(ICreateProcess createProcess)
        {
            this.createProcess = createProcess;
            pathMyStemUtility = Environment.CurrentDirectory + @"\mystem.exe";
            flags = "-nig --format json";
        }
    }
}