using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        private FileSettings fileSettings;
        private readonly ITagCloudService _tagCloudService;

        public SaveImageAction(ITagCloudService tagCloudService, FileSettings fileSettings)
        {
            this._tagCloudService = tagCloudService;
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Сохранить как...";
        public string Description => "Сохранить файл как";

        public void Perform()
        {
            _tagCloudService.SaveImage(fileSettings.ResultImagePath);
        }
    }
}
