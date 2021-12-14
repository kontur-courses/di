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
public static class DIContainersHelper
{
    public static IContainer GetContainer(LayoutSettings settings)
    {
        ExcludeWordFileHelper.LoadWordsFromFile(settings.PathToExcludedWords);
        var circularParameters = new CircularCloudLayoutParameters(
            step: settings.Step,
            minAngle: MathF.PI / 180 * settings.MinAngle);

        Func<string,bool> wordFilter = s => !ExcludeWordFileHelper.IsExclude(s);

        // ReSharper disable once ConvertToLocalFunction
        var builder = new ContainerBuilder();

        builder.RegisterType<TagStatisticMaker>()
            .As<IStatisticMaker>()
            .WithParameter("tagFilter", wordFilter);

        builder.RegisterType<WinTagMaker>().As<ITagMaker>();

        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>()
            .WithParameters(new List<Parameter>
            {
                new PositionalParameter(0, Point.Empty),
                new PositionalParameter(1, circularParameters)
            });

        var fontColor = int.Parse("FF" + settings.FontColor, NumberStyles.HexNumber);
        var backGroundColor = int.Parse("FF" + settings.BackgroundColor, NumberStyles.HexNumber);
        builder.RegisterType<CloudLayouter>().WithParameters(new List<Parameter>(
            new List<Parameter>
            {
                new NamedParameter("fontName", settings.FontName),
                new NamedParameter("maxFontSize", settings.MaxFontSize),
                new NamedParameter("fontColorHex", fontColor),
                new NamedParameter("backgroundColorHex", backGroundColor),
                new NamedParameter("imageSize", settings.PictureSize)
            }
        ));

        return builder.Build();
    }
}