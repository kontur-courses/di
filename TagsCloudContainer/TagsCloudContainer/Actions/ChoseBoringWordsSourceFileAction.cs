using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;

namespace TagsCloudContainer.Actions
{
    public class ChoseBoringWordsSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;
        private readonly ITagCloudService _tagCloudService;

        public ChoseBoringWordsSourceFileAction(ITagCloudService tagCloudService, FileSettings fileSettings)
        {
            this._tagCloudService = tagCloudService;
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Источник скучных слов...";
        public string Description => "Выбрать источник скучных слов для алгоритма";

        public void Perform()
        {
            fileSettings.CustomBoringWordsFilePath = _tagCloudService.SetFilePath(fileSettings.CustomBoringWordsFilePath);
        }
    }
}
