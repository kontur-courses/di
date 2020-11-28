using System;
using System.Drawing;

namespace TagCloud
{
    public class TagInfo
    {
        public readonly string Value;
        public readonly Font Font;
        
        // TODO: сделать менее открытым
        public Rectangle Rectangle;

        // Я не смог придумать как иначе измерять Size тега,
        // потому что мы должны сперва найти расположение тега с помощью Layouter,
        // для чего нам требуется Size, находимый с помощью Graphics. 
        // Есть вариант создать Graphics, на которую в последствии будет нарисовано облако тегов,
        // относительно нее измерять Size, и отложить ее пока все теги не будут разложены с помощью Layouter,
        // но проблема в этом случае 
        public Size Size
        {
            get
            {
                using var graphicsForMeasure = Graphics.FromImage(bitmapForMeasure);
                var size = graphicsForMeasure
                    .MeasureString(Value, Font);
                return new Size((int)Math.Ceiling(size.Width),
                    (int)Math.Ceiling(size.Height));
            }
        }
        
        private static Bitmap bitmapForMeasure = new Bitmap(1, 1);
        
        public TagInfo(string value, int fontSize)
        {
            Value = value;
            Font = new Font(FontFamily.GenericMonospace, fontSize);
        }
    }
}