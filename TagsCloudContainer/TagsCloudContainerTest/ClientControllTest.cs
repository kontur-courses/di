using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;

namespace TagsCloudContainerTest
{
    public class ClientControllTest
    {
        private IClient client;
        private IWordCloudSaver saver;

        [SetUp]
        public void InitializeServices()
        {
            client = A.Fake<IClient>();
            saver = A.Fake<IWordCloudSaver>();
        }

        [Test]
        public void CheckCreatingImageSettings()
        {
            var clientControl = new ClientControl(client, saver);

            clientControl.GetImageSettings();

            A.CallTo(() => client.GetBackgoundColor()).MustHaveHappenedOnceExactly();
            A.CallTo(() => client.GetImageSize()).MustHaveHappenedOnceExactly();
            A.CallTo(() => client.GetFontFamily()).MustHaveHappenedOnceExactly();
            A.CallTo(() => client.GetTextColor()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CheckCallsGetNameForImage()
        {
            var clientControl = new ClientControl(client, saver);

            clientControl.GetNameForImage();

            A.CallTo(() => client.GetNameForImage()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CheckCallsShowPathToImage()
        {
            string name = null;
            A.CallTo(() => client.ShowPathToNewFile(name)).WithAnyArguments();
            var clientControl = new ClientControl(client, saver);

            clientControl.ShowPathToImage(name);

            A.CallTo(() => client.ShowPathToNewFile(name)).MustHaveHappenedOnceExactly();
        }

    }
}
