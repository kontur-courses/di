using System.Collections.Generic;

namespace TagCloud.repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly IEnumerable<Tag> tags;
        
        public TagRepository(IEnumerable<Tag> tags)
        {
            this.tags = tags;
        }

        public IEnumerable<Tag> Get() => tags;
    }
}