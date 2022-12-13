namespace TagsCloud.Core.Settings;

public class SettingsProvider<TSettings> : ISettingsGetter<TSettings>, ISettingsSetter<TSettings>
{
	private TSettings settings;

	public TSettings Get()
	{
		return settings;
	}

	public void Set(TSettings settings)
	{
		this.settings = settings;
	}
}