using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer
{
    [TestFixture]
    class BoringWordsFormater_Should
    {
        private BoringWordsFormater boringWordsFormater;
        private string[] boringWords;

        [SetUp]
        public void SetUp()
        {
            boringWords = new string[] { "по", "да", "и", "сейчас" };
            boringWordsFormater = new BoringWordsFormater(boringWords);
        }

        [Test]
        public void Array_NotContains_BoringWords()
        {
            boringWordsFormater.HandleWords(new string[] { "по", "день", "утро", "вечер" }).Should()
                .NotContain(boringWords);
        }
    }
}
