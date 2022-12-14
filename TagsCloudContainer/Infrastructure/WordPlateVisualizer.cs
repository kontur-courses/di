using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordColorProviders.Factories;

namespace TagsCloudContainer.Infrastructure
{
    public class WordPlateVisualizer
    {
        private readonly IWordColorProviderFactory colorProviderFactory;

        public WordPlateVisualizer(IWordColorProviderFactory colorProviderFactory)
        {
            this.colorProviderFactory = colorProviderFactory;
        }

        public Bitmap DrawPlates(WordPlate[] plates, Size size, WordColorSettings settings)
        {
            if (size.IsEmpty)
                throw new ArgumentException("Size can't be empty");

            var colorProvider = colorProviderFactory.CreateDefault(settings);

            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            foreach(var plate in plates)
                graphics.DrawString(plate.WordRectangle.Word, plate.Font, new SolidBrush(colorProvider.GetColor(plate.WordRectangle.Word)), plate.WordRectangle.Rectangle);

            return bitmap;
        }

        public void DrawPlatesAndSave(WordPlate[] plates, Size size, string filename, WordColorSettings settings)
        {
            using var bitmap = DrawPlates(plates, size, settings);
            bitmap.Save(filename);
        }
    }
}