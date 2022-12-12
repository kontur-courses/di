using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Visualisator;

namespace TagsCloudContainer.Actions
{
    public class DrawImageAction : IUiAction
    {
        private IPainter painter;
        private ICloudLayouter cloudLayouter;
        private ImageSettings imageSettings;
        private readonly IWordsCounter wordscounter;
        private readonly FileSettings fileSettings;

        public DrawImageAction(ICloudLayouter cloudLayouter, IPainter painter, ImageSettings imageSettings, 
            IWordsCounter wordsCounter, FileSettings fileSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.painter = painter;
            this.imageSettings = imageSettings;
            this.wordscounter = wordsCounter;
            this.fileSettings = fileSettings;
        }

        public string Category => "Изображение";
        public string Name => "Получить картинку";
        public string Description => "Отрисовывает Облако тегов";

        public void Perform()
        {
            var wordsCount = wordscounter.CountWords(fileSettings.SourceFilePath
                , fileSettings.CustomBoringWordsFilePath);
            painter.Paint(cloudLayouter.FindRectanglesPositions(imageSettings.Width, imageSettings.Height, wordsCount));
        }
    }
}
