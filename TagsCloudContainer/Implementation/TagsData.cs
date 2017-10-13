using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TagsData : ITagsData
    {
        private IFileParser parser;
        private IWordPreprocessor preprocessor;

        public TagsData(IFileParser parser, IWordPreprocessor preprocessor)
        {
            this.parser = parser;
            this.preprocessor = preprocessor;
        }

        public IEnumerable<string> GetData()
        {
            return preprocessor.Handle(parser.ReadLinesToArray());
        }
    }
}
