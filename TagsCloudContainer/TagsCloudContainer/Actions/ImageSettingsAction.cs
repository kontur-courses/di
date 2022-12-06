using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        public string Category => "Файл";
        public string Name => "Источник данных...";
        public string Description => "Выбрать источник данных для алгоритма";

        public void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
