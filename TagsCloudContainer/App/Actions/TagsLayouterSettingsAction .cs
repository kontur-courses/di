using System;
using Autofac;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.App.Actions
{
    public class TagsLayouterSettingsAction : IUiAction
    {
        private readonly CloudLayouterSettings settings;

        public TagsLayouterSettingsAction(CloudLayouterSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Облако тегов";
        public string Name => "Настройки";
        public string Description => "Настройки для облака тегов";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}