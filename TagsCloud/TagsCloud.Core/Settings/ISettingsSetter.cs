namespace TagsCloud.Core.Settings;

public interface ISettingsSetter<in TSettings>
{
	public void Set(TSettings settings);
}