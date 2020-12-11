using System.Collections.Generic;
using System.Drawing;
using FakeItEasy;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class AddColorCommand_Should
    {
        private AddColorCommand command;
        private IImageColorProvider imageColorProvider;

        [SetUp]
        public void SetUp()
        {
            imageColorProvider = A.Fake<IImageColorProvider>();
            command = new AddColorCommand(imageColorProvider);
        }

        [Test]
        public void Execute_CallMethod_AddColors()
        {
            command.Execute(new[] {"blue"});
            A.CallTo(() => imageColorProvider.AddColors(A<IEnumerable<Color>>.Ignored))
                .MustHaveHappened();
        }
    }
}