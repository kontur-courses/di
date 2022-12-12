using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;
using TagsCloudContainer.Visualisator;

namespace TagsCloudContainer.Actions
{
    public class DrawImageAction : IUiAction
    {
        private ImageSettings imageSettings;
        private readonly FileSettings fileSettings;
        private readonly ITagCloudService _tagCloudService;

        public DrawImageAction(ITagCloudService tagCloudService, ICloudLayouter cloudLayouter, IPainter painter, ImageSettings imageSettings, 
            IWordsCounter wordsCounter, FileSettings fileSettings)
        {
            this._tagCloudService = tagCloudService;
            this.imageSettings = imageSettings;
            this.fileSettings = fileSettings;
        }

        public string Category => "Изображение";
        public string Name => "Получить картинку";
        public string Description => "Отрисовывает Облако тегов";

        public void Perform()
        {
            _tagCloudService.DrawImage(fileSettings.SourceFilePath, fileSettings.CustomBoringWordsFilePath,
                imageSettings.Width, imageSettings.Height);
        }
    }
}
