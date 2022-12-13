namespace TagsCloud.Core.Settings;

public interface ISettingsGetter<out TSettings>
{
	public TSettings Get();
}