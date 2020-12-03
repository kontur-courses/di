using System.Collections.Generic;

namespace HomeExerciseTDD
{
    public interface IFileProcessor
    {
        public Dictionary<string, int> GetWords();
    }
}