using System.IO;
using System.Linq;

namespace TagCloud
{
    public class OneWordInLineParser : IWordParser
    {
        private IPathCreater creater;
        public OneWordInLineParser(IPathCreater creater)
        {
            this.creater = creater;
        }
        
        public string[] GetWords(string inputFileName)
        {
            //TODO: Add filterWords
            
            try
            {
                return File.ReadLines(creater.GetPathToFile(inputFileName)).ToArray();
            }
            catch (FileNotFoundException)
            {
                //TODO: handle exception
            }

            return new string[0];
        }
    }
}