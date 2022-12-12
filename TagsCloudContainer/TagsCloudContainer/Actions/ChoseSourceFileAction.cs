using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;

namespace TagsCloudContainer.Actions
{
    public class ChoseSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;
        private readonly IService service;

        public ChoseSourceFileAction(IService service, FileSettings fileSettings)
        {
            this.service = service;
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Источник данных...";
        public string Description => "Выбрать источник данных для алгоритма";

        public void Perform()
        {
            fileSettings.SourceFilePath = service.SetFilePath(fileSettings.SourceFilePath);
        }
    }
}
