using TagsCloud.App.Settings;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.App.Actions;

public class WordAnalyzerSettingsAction : IUiAction
{
    private readonly WordAnalyzerSettings wordAnalyzerSettings;

    public WordAnalyzerSettingsAction(WordAnalyzerSettings wordAnalyzerSettings)
    {
        this.wordAnalyzerSettings = wordAnalyzerSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Анализатор...";
    public string Description => "";

    public void Perform()
    {
        SettingsForm.For(wordAnalyzerSettings).ShowDialog();
    }
}