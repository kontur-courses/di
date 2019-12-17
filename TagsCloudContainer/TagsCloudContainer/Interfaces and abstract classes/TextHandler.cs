using Autofac;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public abstract class TextHandler
    {
        protected readonly IDullWordsEliminator dullWordsEliminator;
        protected readonly ITextReader textReader;

        public TextHandler(ITextReader textReader, IDullWordsEliminator dullWordsEliminator)
        {
            this.dullWordsEliminator = dullWordsEliminator;
            this.textReader = textReader;
        }

        public abstract Dictionary<string, int> GetWordsFrequencyDict();
    }
}