using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CommandLine;
using TagCloud;
using Extensions;

namespace ConsoleTagClouder
{
    public class AppSettings
    {
        private (int x, int y) size = (-1, -1);

        [Value(0)]
        public string SourcePath { get; set; }
        
        [Value(1)]
        public string TargetPath { get; set; }

        [Option('s', Separator = ';')]
        private (int x, int y) Size
        {
            get => size;
            set
            {
                if (value.x < 0 || value.y < 0)
                    throw new ArgumentException("Size must be positive");
                size = value;
            }
        }

        [Option('t')]
        private string FontType { get; set; }
        [Option('b')]
        private string FontBrush { get; set; }
        [Option('g')]
        private string BackgroundBrush { get; set; }
                
        [Option('l')]
        private string TLayouter { get; set;  }
        [Option('c')]
        private string TCounter { get; set;  }
        [Option('m')]
        private string TScaler { get; set;  }

        public CloudSettings BuildCloudSettings()
        {
            var settings = CloudSettings.Default();

            if (TLayouter != null)
                settings.TLayouter = TLayouter;// FindInterfacesWithPrefix<ICloudLayouter>(TLayouter).TakeOnlyOne();
            if (TCounter != null)
                settings.TCounter = TCounter;//FindInterfacesWithPrefix<IWordsCounter>(TCounter).TakeOnlyOne();
            if (TScaler != null)
                settings.TScaler = TScaler;//FindInterfacesWithPrefix<IWordsCounter>(TCounter).TakeOnlyOne();
            
            return settings;
        }
        
        public DrawingSettings BuildDrawingSettings()
        {
            var settings = DrawingSettings.Default();

            if (size.x > 0 && size.y > 0)
                settings.Size = new Size(Size.x, Size.y);
            if (BackgroundBrush != null)
                settings.BackgroundBrush = new SolidBrush(Color.FromName(BackgroundBrush));
            if (FontBrush != null)
                settings.FontBrush = new SolidBrush(Color.FromName(FontBrush));
            if (FontType != null)
                settings.FontType = FontType;
            
            return settings;
        }

        //TODO remove comments
//        private IEnumerable<Type> FindInterfacesWithPrefix<TInterface>(string prefix)=>
//            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
//                .Where(t => typeof(TInterface).IsAssignableFrom(t))
//                .Where(n=>n.Name.ToLower().StartsWith(prefix.ToLower()));
           
    }
}