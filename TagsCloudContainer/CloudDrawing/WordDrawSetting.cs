using System.Drawing;

namespace CloudDrawing
{
    public class WordDrawSettings
    {
        public WordDrawSettings(string familyName, Brush brush,  StringFormat stringFormat, bool haveDelineation)
        {
            FamilyName = familyName;
            Brush = brush;
            StringFormat = stringFormat;
            HaveDelineation = haveDelineation;
        }
        
        public string FamilyName { get;}
        public Brush Brush { get; }
        public StringFormat StringFormat { get; }
        public bool HaveDelineation { get; }
        
    }
}