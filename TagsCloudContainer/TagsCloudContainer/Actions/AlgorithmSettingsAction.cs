using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class AlgorithmSettingsAction : IUiAction
    {
        private AlgorithmSettings algoSettings;

        public AlgorithmSettingsAction(AlgorithmSettings algoSettings)
        {
            this.algoSettings = algoSettings;
        }

        public string Category => "Алгоритм";
        public string Name => "Настройки...";
        public string Description => "Изменить настройки алгоритма";
        public void Perform()
        {
            SettingsForm.For(algoSettings).ShowDialog();
        }
    }
}
