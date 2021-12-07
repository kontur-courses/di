using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.TextPreparers
{
    public class TextPreparerBuilder : ITextPreparerBuilder
    {
        private readonly List<Func<string, bool>> filters = new();
        private readonly List<Func<string, string>> preparations = new();

        public ITextPreparerBuilder WithFilter(Func<string, bool> filter)
        {
            filters.Add(filter);
            return this;
        }

        public ITextPreparerBuilder WithPreparation(Func<string, string> preparation)
        {
            preparations.Add(preparation);
            return this;
        }

        public ITextPreparer Build()
        {
            return new TextPreparer(filters, preparations);
        }
    }
}