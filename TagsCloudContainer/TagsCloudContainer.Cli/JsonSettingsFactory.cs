using System.Drawing;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Cli;

public class JsonSettingsFactory : ISettingsFactory
{
    private readonly string jsonSettingsFileName;

    private readonly JsonSerializerOptions options = new(JsonSerializerOptions.Default)
    {
        TypeInfoResolver = new SettingsTypeResolver(),
        IgnoreReadOnlyProperties = true,
        IgnoreReadOnlyFields = true,
        Converters = { new JsonColorConverter() },
        WriteIndented = true
    };

    public JsonSettingsFactory(string jsonSettingsFileName)
    {
        this.jsonSettingsFileName = jsonSettingsFileName;
    }

    public Settings Build()
    {
        var settings = new Settings();

        if (!File.Exists(jsonSettingsFileName))
        {
            using var fileWriteStream = File.OpenWrite(jsonSettingsFileName);
            JsonSerializer.Serialize(fileWriteStream, settings, options);
            return settings;
        }

        using var fileStream = File.OpenRead(jsonSettingsFileName);
        return JsonSerializer.Deserialize<Settings>(fileStream, options) ?? throw new InvalidOperationException();
    }

    private class SettingsTypeResolver : DefaultJsonTypeInfoResolver
    {
        private static readonly Dictionary<Type, JsonDerivedType[]> JsonDerivedTypes;

        static SettingsTypeResolver()
        {
            var tagCloudAssemblies = new[]
            {
                Assembly.Load("TagsCloudContainer"),
                Assembly.Load("TagsCloudContainer.Interfaces"),
                Assembly.Load("TagsCloudContainer.Cli")
            };
            JsonDerivedTypes = new()
            {
                {
                    typeof(GraphicsProviderSettings),
                    GetJsonDerivedTypes(typeof(GraphicsProviderSettings), tagCloudAssemblies)
                },
                {
                    typeof(DrawerSettings), GetJsonDerivedTypes(typeof(DrawerSettings),
                        tagCloudAssemblies)
                },
                {
                    typeof(LayouterAlgorithmSettings), GetJsonDerivedTypes(typeof(LayouterAlgorithmSettings),
                        tagCloudAssemblies)
                },
                { typeof(Brush), GetJsonDerivedTypes(typeof(Brush), typeof(Brush).Assembly) },
                { typeof(FontFamily), GetJsonDerivedTypes(typeof(FontFamily), typeof(FontFamily).Assembly) }
            };
        }

        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            var jsonTypeInfo = base.GetTypeInfo(type, options);

            if (!JsonDerivedTypes.ContainsKey(jsonTypeInfo.Type)) return jsonTypeInfo;

            jsonTypeInfo.PolymorphismOptions = new()
            {
                TypeDiscriminatorPropertyName = "Type",
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
            };
            foreach (var jsonDerivedType in JsonDerivedTypes[jsonTypeInfo.Type])
                jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(jsonDerivedType);

            return jsonTypeInfo;
        }

        private static JsonDerivedType[] GetJsonDerivedTypes(Type targetType, params Assembly[] assemblies)
        {
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(targetType))
                .Select(t => new JsonDerivedType(t, t.Name)).ToArray();
        }
    }
}