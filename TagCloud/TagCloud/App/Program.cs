using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TagCloud
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = BuildContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = container.Resolve<MainForm>();
            Application.Run(mainForm);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var filters = new HashSet<IFilter>(GetIFilterImplementations());
            var filterList = new FilterList(filters, filters);
            var parsers = new HashSet<IParser>(GetIParserImplementations());
            var parserList = new ParserList(parsers, parsers);
            var themes = new HashSet<ITheme>(GetIThemeImplementations());
            var themeList = new ThemeList(themes, themes);
            var boringWords = new HashSet<string>(File.ReadAllLines($"{HelperMethods.GetProjectDirectory()}\\BoringWords.txt"));
            var boringWordsList = new BoringWordsList(boringWords, boringWords);
            var speechParts = new HashSet<SpeechPart>(Enum.GetValues(typeof(SpeechPart)) as SpeechPart[]);
            var speechPartList = new SpeechPartList(speechParts, speechParts);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IUiAction).IsAssignableFrom(t)).As<IUiAction>();
            builder.RegisterType<DefaultExtractor>().SingleInstance().As<IExtractor>();
            builder.RegisterType<ArchimedeanSpiralLayouter>().SingleInstance().As<ILayouter>();
            builder.RegisterType<LowerCaseParser>().SingleInstance().As<IParser>();
            builder.RegisterType<DefaultTxtReader>().SingleInstance().As<IReader>();
            builder.RegisterType<AppVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<ImageHolder>().SingleInstance().As<IImageHolder>().As<PictureBox>().As<ImageHolder>();
            builder.Register(context => FileSettings.GetDefaultSettings()).SingleInstance().As<FileSettings>();
            builder.Register(context => ImageSettings.GetDefaultSettings()).SingleInstance().As<ImageSettings>();
            builder.Register(context => FontSettings.GetDefaultSettings()).SingleInstance().As<FontSettings>();
            builder.Register(context => LayouterSettings.GetDefaultSettings()).SingleInstance().As<LayouterSettings>();
            builder.Register(context => parserList).SingleInstance().As<IItemList<IParser>>().As<ParserList>();
            builder.Register(context => filterList).SingleInstance().As<IItemList<IFilter>>().As<FilterList>();
            builder.Register(context => speechPartList).SingleInstance().As<IItemList<SpeechPart>>().As<SpeechPartList>();
            builder.Register(context => themeList).SingleInstance().As<IItemList<ITheme>>().As<ThemeList>();
            builder.Register(context => boringWordsList).SingleInstance().As<IItemList<string>>().As<BoringWordsList>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();
            return builder.Build();
        }

        private static IEnumerable<IFilter> GetIFilterImplementations()
        {
            var builder = new ContainerBuilder();
            var boringWords = new HashSet<string>(File.ReadAllLines($"{HelperMethods.GetProjectDirectory()}\\BoringWords.txt"));
            var boringWordsList = new BoringWordsList(boringWords, boringWords);
            var speechParts = new HashSet<SpeechPart>(Enum.GetValues(typeof(SpeechPart)) as SpeechPart[]);
            var speechPartList = new SpeechPartList(speechParts, speechParts);
            builder.Register(context => speechPartList).SingleInstance().As<IItemList<SpeechPart>>().As<SpeechPartList>();
            builder.Register(context => boringWordsList).SingleInstance().As<IItemList<string>>().As<BoringWordsList>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IFilter).IsAssignableFrom(t)).As<IFilter>();
            var container = builder.Build();
            return container.Resolve<IEnumerable<IFilter>>(); ;
        }

        private static IEnumerable<IParser> GetIParserImplementations()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IParser).IsAssignableFrom(t)).As<IParser>();
            var container = builder.Build();
            return container.Resolve<IEnumerable<IParser>>();
        }

        private static IEnumerable<ITheme> GetIThemeImplementations()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(ITheme).IsAssignableFrom(t)).As<ITheme>();
            var container = builder.Build();
            return container.Resolve<IEnumerable<ITheme>>();
        }
    }
}
