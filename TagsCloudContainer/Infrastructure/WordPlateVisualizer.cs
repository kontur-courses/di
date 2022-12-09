using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class WordPlateVisualizer
    {
        private readonly IWordColorProvider colorProvider;

        public WordPlateVisualizer(IWordColorProvider colorProvider)
        {
            this.colorProvider = colorProvider;
        }

        public Bitmap DrawPlates(WordPlate[] plates, Size size)
        {
            if (size.IsEmpty)
                throw new ArgumentException("Size can't be empty");

            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            foreach(var plate in plates)
                graphics.DrawString(plate.WordRectangle.Word, plate.Font, new SolidBrush(colorProvider.GetColor(plate.WordRectangle.Word)), plate.WordRectangle.Rectangle);

            return bitmap;
        }

        public void DrawPlatesAndSave(WordPlate[] plates, Size size, string filename)
        {
            using var bitmap = DrawPlates(plates, size);
            bitmap.Save(filename);
        }
    }
}