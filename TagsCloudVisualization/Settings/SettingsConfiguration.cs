using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.FileReader;

namespace TagsCloudVisualization.Settings
{
    public abstract class SettingsConfiguration
    {
        public abstract TextFileReader FileReader { get; }

    }
}
