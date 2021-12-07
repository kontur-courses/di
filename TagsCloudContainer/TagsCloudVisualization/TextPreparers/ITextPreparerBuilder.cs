using System;

namespace TagsCloudVisualization.TextPreparers
{
    public interface ITextPreparerBuilder
    {
        public ITextPreparerBuilder WithFilter(Func<string, bool> filter);

        public ITextPreparerBuilder WithPreparation(Func<string, string> preparation);

        public ITextPreparer Build();
    }
}