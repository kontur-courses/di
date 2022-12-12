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
        private double dr = 0.01;
        private double fi = 0.0368;

        [DisplayName("Скорость отдаления спирали от центра")]
        public double Dr 
        {
            get => dr;
            set => dr = value > 0 ? value : dr;
        }
        [DisplayName("Скорость изменения угла")]
        public double Fi
        {
            get => fi;
            set => fi = value > 0 ? value : fi;
        }
    }
}
