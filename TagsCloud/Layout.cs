using System.Collections.Generic;

namespace TagsCloud
{
	public class Layout
	{
		public IEnumerable<Tag> Tags { get; }

		public Layout(IEnumerable<Tag> tags) => Tags = tags;
	}
}