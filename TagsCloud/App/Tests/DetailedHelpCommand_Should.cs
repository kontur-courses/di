using System;
using System.IO;
using FakeItEasy;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class DetailedHelpCommand_Should
    {
        private IClient client;
        private DetailedHelpCommand command;
        private TextWriter writer;

        [SetUp]
        public void SetUp()
        {
            client = A.Fake<IClient>();
            writer = A.Fake<TextWriter>();
            command = new DetailedHelpCommand(new Lazy<IClient>(client), writer);
        }

        [Test]
        public void Execute_CallMethod_FindCommandByName()
        {
            command.Execute(new[] {"asd"});
            A.CallTo(() => client.FindCommandByName(A<string>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Execute_CallMethod_WriteLine()
        {
            A.CallTo(() => client.FindCommandByName("help")).Returns(command);
            command.Execute(new[] {"help"});
            A.CallTo(() => writer.WriteLine(A<string>.Ignored)).MustHaveHappened();
        }
    }
}