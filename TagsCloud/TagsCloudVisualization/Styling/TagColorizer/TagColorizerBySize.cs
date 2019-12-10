namespace TagsCloudVisualization.Styling.TagColorizer
 {
     public class TagColorizerBySize : ITagColorizer
     {
         public string GetTagColor(string[] tagColors, Tag tag)
         {
             return tagColors[tag.Count % tagColors.Length];
         }
     }
 }