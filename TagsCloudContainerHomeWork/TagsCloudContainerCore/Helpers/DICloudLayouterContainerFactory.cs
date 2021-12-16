using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Autofac;
using Autofac.Core;
using TagsCloudContainerCore.CircularLayouter;
using TagsCloudContainerCore.Console;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore.Helpers;

// ReSharper disable once InconsistentNaming
public static class DICloudLayouterContainerFactory
{
    public static IContainer GetContainer(LayoutSettings settings)
    {
        ExcludeWordFileHelper.LoadWordsFromFile(settings.PathToExcludedWords);
        var circularParameters = new CircularCloudLayoutParameters(
            step: settings.Step,
            minAngle: MathF.PI / 180 * settings.MinAngle);

        var backgroundColor = int.Parse("FF" + settings.BackgroundColor, NumberStyles.HexNumber);

        // ReSharper disable once ConvertToLocalFunction
        Func<string, bool> wordFilter = s => !ExcludeWordFileHelper.IsExclude(s);

        var builder = new ContainerBuilder();

        builder.RegisterType<WinPainter>().As<IPainter>()
            .WithParameter(new NamedParameter("backgroundColorHex", backgroundColor));

        builder.RegisterType<TagStatisticMaker>()
            .As<IStatisticMaker>()
            .WithParameter("tagFilter", wordFilter);


        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>()
            .WithParameters(new List<Parameter>
            {
                new PositionalParameter(0, Point.Empty),
                new PositionalParameter(1, circularParameters)
            });
        
        builder.RegisterType<WinTagMaker>().As<ITagMaker<LayoutSettings>>();

        builder.RegisterType<TagCloudMaker>().As<ITagCloudMaker<LayoutSettings>>();

        return builder.Build();
    }
}