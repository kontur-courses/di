using System;
using System.Collections.Generic;
using System.Drawing;
using SyntaxTextParser;
using TagsCloudGenerator.CloudPrepossessing;

namespace TagsCloudGenerator
{
    public static class CloudGenerator
    {
        private const double HeightToWidthIndexMultiplier = 0.6;

        public static List<CloudTag> CreateTagsCloud(List<CountedTextElement> elements, 
            ITagsPrepossessing tagPlacer, CloudFormat cloudFormat)
        {
            elements = cloudFormat.TagOrderPreform.OrderEnumerable(elements);
            var result = new List<CloudTag>();

            foreach (var element in elements)
            {
                var width = (int)(element.Word.Length * element.Count * HeightToWidthIndexMultiplier);
                var height = element.Count;
                var rect = tagPlacer.PutNextRectangle(new Size(width, height));

                result.Add(new CloudTag(rect, element.Word,
                    cloudFormat.TagTextFormat, cloudFormat.TagTextFont));
            }

            return result;
        }
    }
}