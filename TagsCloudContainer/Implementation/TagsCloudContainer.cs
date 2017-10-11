using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TagsCloudContainer
    {
        private ITagsData tagsData;
        private ICircularCloudLayouter circularCloudLayouter;

        public TagsCloudContainer(ITagsData tagsData, ICircularCloudLayouter circularCloudLayouter)
        {
            this.tagsData = tagsData;
            this.circularCloudLayouter = circularCloudLayouter;
        }






    }
}
