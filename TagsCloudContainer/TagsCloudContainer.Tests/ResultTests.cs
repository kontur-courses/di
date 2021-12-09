using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    public class ResultTests
    {
        [Test]
        public void GetValue_ReturnValue_IfResultSucceed()
        {
            var value = new List<string> {"string"};
            var result = new Result<List<string>>(value);
            result.Value
                .Should().BeSameAs(value);
        }

        [Test]
        public void GetException_ReturnException_IfResultNotSucceed()
        {
            var exception = new Exception("exception");
            var result = new Result<int>(exception);
            result.Exception
                .Should().Be(exception);
        }

        [Test]
        public void GetValue_ThrowsException_IfResultNotSucceed()
        {
            var result = new Result<int>(new Exception());
            Assert.Throws<Exception>(
                () =>
                {
                    var _ = result.Value;
                });
        }

        [Test]
        public void GetException_ThrowsException_IfResultSucceed()
        {
            var result = new Result<int>(1);
            Assert.Throws<Exception>(
                () =>
                {
                    var _ = result.Exception;
                });
        }
    }
}