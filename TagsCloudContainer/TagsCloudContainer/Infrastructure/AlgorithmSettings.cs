using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class AlgorithmSettings
    {
        [DisplayName("Скорость отдаления спирали от центра")]
        public double Dr { get; set; } = 0.01;
        [DisplayName("Скорость изменения угла")]
        public double Fi { get; set; } = 0.0368;

        public void ThrowExcIfNonPositiveArgs()
        {
            if (Dr <= 0)
                throw new ArgumentException($"Параметры только положительны! Dr={Dr}");
            if (Fi <= 0)
                throw new ArgumentException($"Параметры только положительны! Fi={Fi}");
        }
    }
}
