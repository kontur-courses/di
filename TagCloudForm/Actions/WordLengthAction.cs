using TagCloud.TextFilterConditions;
using TagCloud.Visualization;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class WordLengthAction : IUiAction
    {
        private readonly CloudVisualization cloudVisualization;
        private readonly WordLengthCondition wordLengthCondition;
        private readonly CloudPainter cloudPainter;

        public WordLengthAction(CloudVisualization cloudVisualization, WordLengthCondition wordLengthCondition,
            CloudPainter cloudPainter)
        {
            this.cloudVisualization = cloudVisualization;
            this.wordLengthCondition = wordLengthCondition;
            this.cloudPainter = cloudPainter;
        }

        public string Category => "Длина слова";
        public string Name => "Настроить";
        public string Description => "Установить длину слова";

        public void Perform()
        {
            SettingsForm<WordLengthCondition>.For(wordLengthCondition).ShowDialog();
            cloudVisualization.ResetWordsFrequenciesDictionary();
            cloudPainter.Paint();
        }
    }
}