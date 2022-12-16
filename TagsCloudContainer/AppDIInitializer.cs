using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.WordsInterfaces;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public static class AppDIInitializer
{
    private static readonly ServiceCollection _service;

    static AppDIInitializer()
    {
        _service = new ServiceCollection();
        _service.AddTransient<IWordsReader, WordsReader>();
        _service.AddTransient<IWordsAnalyzer, WordsAnalyzer>();
        _service.AddTransient<IWordsFrequencyCounter, WordsFrequencyCounter>();
        _service.AddTransient<IWordsCollector, WordsCollector>();
        
        _service.AddTransient<ICurve, ArchimedeanSpiral>();
        _service.AddTransient<ILayouter, CircularCloudLayouter>();
        _service.AddTransient<ISizeManager, CircularLayouterSizeManager>();
        _service.AddTransient<IDrawingModel, DrawingModel>();
        _service.AddSingleton<LayoutDrawer>();

        Container = _service.BuildServiceProvider();
    }

    public static ServiceProvider Container { get; private set; }

    public static void CreateCurveInstance(double step, double density, double start)
    {
        _service.AddSingleton<ICurve>(sp => new ArchimedeanSpiral(step, density, start));
        Container = _service.BuildServiceProvider();
    }
}