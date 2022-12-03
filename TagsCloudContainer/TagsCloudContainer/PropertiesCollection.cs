namespace TagsCloudContainer;

public class PropertiesCollection
{
    public IReadOnlyCollection<Dictionary<string, string>> LayouterAlgorithmsSettingsDictionary { get; set; } =
        new List<Dictionary<string, string>>();

    public IReadOnlyCollection<Dictionary<string, string>> DrawerSettingsDictionary { get; set; } =
        new List<Dictionary<string, string>>();

    private Dictionary<string, string> GraphicsSettings { get; set; } = new();
    //
    // public void Set(string name, string value)
    // {
    //     values[name] = value;
    // }
    //
    // public T Get<T>(string name, Func<string, T> parse)
    // {
    //     return parse(values[name]);
    // }
    //
    // public T GetOrDefault<T>(string name, Func<string, T> parse, T defaultValue = default!)
    // {
    //     _ = TryGet(name, parse, out var result, defaultValue);
    //     return result!;
    // }
    //
    // public bool TryGet<T>(string name, Func<string, T> parse, out T? result, T? defaultValue = default)
    // {
    //     result = defaultValue;
    //     if (!values.ContainsKey(name))
    //         return false;
    //     try
    //     {
    //         result = Get(name, parse);
    //         return true;
    //     }
    //     catch
    //     {
    //         return false;
    //     }
    // }
}