using System.IO;
using System.Linq;

namespace TagCloud
{
    public class OneWordInLineParser : IWordParser
    {
        private IPathCreater Creater;
        public OneWordInLineParser(IPathCreater creater)
        {
            Creater = creater;
        }
        
        public string[] GetWords(string inputFileName)
        {
            //TODO: Add filterWords
            
            try
            {
                return File.ReadLines(Creater.GetPathToFile(inputFileName)).ToArray();
            }
            catch (FileNotFoundException)
            {
                //TODO: handle exception
            }

            return new string[0];
        }
    }
}