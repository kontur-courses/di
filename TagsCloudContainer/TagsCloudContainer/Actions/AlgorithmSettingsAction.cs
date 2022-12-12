using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;

namespace TagsCloudContainer.Actions
{
    public class AlgorithmSettingsAction : IUiAction
    {
        private AlgorithmSettings algoSettings;
        private readonly IService service;

        public AlgorithmSettingsAction(IService service, AlgorithmSettings algoSettings)
        {
            this.service = service;
            this.algoSettings = algoSettings;
        }

        public string Category => "Алгоритм";
        public string Name => "Настройки...";
        public string Description => "Изменить настройки алгоритма";
        public void Perform()
        {
            service.SetSettings(algoSettings);
        }
    }
}
