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
        [AppSettingsOption]
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
        [AppSettingsOption]
        private string FontType { get; set; }
        [Option('b')]
        [AppSettingsOption]
        private string FontBrush { get; set; }
        [Option('g')]
        [AppSettingsOption]
        private string BackgroundBrush { get; set; }
                
        [Option('l')]
        [AppSettingsOption]
        private string TLayouter { get; set;  }
        [Option('c')]
        [AppSettingsOption]
        private string TCounter { get; set;  }

        public ClouderSettings BuildClouderSettings()
        {
            var settings = ClouderSettings.Default();

            //TODO make it pretty 
            if (TLayouter != null)
                settings.TLayouter = FindInterfacesWithPrefix<ICloudLayouter>(TLayouter).TakeOnlyOne();
            if (TCounter != null)
                settings.TCounter = FindInterfacesWithPrefix<IWordsCounter>(TCounter).TakeOnlyOne();
            if (size.x > 0 && size.y > 0)
                settings.DrawingSettings.Size = new Size(Size.x, Size.y);
            if (BackgroundBrush != null)
                settings.DrawingSettings.BackgroundBrush = new SolidBrush(Color.FromName(BackgroundBrush));
            if (FontBrush != null)
                settings.DrawingSettings.FontBrush = new SolidBrush(Color.FromName(FontBrush));
            if (FontType != null)
                settings.DrawingSettings.FontType = FontType;
            
            return settings;
        }

        private IEnumerable<Type> FindInterfacesWithPrefix<TInterface>(string prefix)=>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(t => typeof(TInterface).IsAssignableFrom(t))
                .Where(n=>n.Name.ToLower().StartsWith(prefix.ToLower()));
           
    }
}