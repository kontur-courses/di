using System.Collections.Generic;
using System.Drawing;

namespace HomeExerciseTDD
{
    public interface IWordCloud
    {
        List<ISizedWord> SizedWords { get; }
        Point Center { get; }

        public void BuildCloud();
    }
}