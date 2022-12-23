using FluentResults;
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

        public Result<Bitmap> DrawPlates(WordPlate[] plates, Size size, WordColorSettings settings)
        {
            return Result.OkIf(size.Width != 0 && size.Height != 0, "Size dimensions can't be empty")
                         .Bind(() => GetBitmap(plates, size, settings));
        }

        private Result<Bitmap> GetBitmap(WordPlate[] plates, Size size, WordColorSettings settings)
        {
            var colorProvider = colorProviderFactory.CreateDefault(settings);

            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            foreach (var plate in plates)
            {
                var colorResult = colorProvider.GetColor(plate.WordRectangle.Word)
                                               .OnSuccess(color => graphics.DrawString(plate.WordRectangle.Word, plate.Font, new SolidBrush(color.Value), plate.WordRectangle.Rectangle));
                if (colorResult.IsFailed)
                    return colorResult.ToResult();
            }

            return Result.Ok(bitmap);
        }

        public Result DrawPlatesAndSave(WordPlate[] plates, Size size, string filename, WordColorSettings settings)
        {
            var result = DrawPlates(plates, size, settings);
            if (result.IsFailed)
                return result.ToResult();

            using var bitmap = result.Value!;
            bitmap.Save(filename);
            return Result.Ok();
        }
    }
}