using System;

namespace TagsCloudContainer.Common
{
    public class TagReader
    {
        private readonly IBlobStorage storage;
        private readonly ISerializer serializer;

        public TagReader(IBlobStorage storage, ISerializer serializer)
        {
            this.storage = storage;
            this.serializer = serializer;
        }

        public string Read(string name)
        {
            var data = storage.Get(name);
            if (data == null)
            {
                throw new Exception("не удалось загрузить файл с тэгами");
            }

            return serializer.Deserialize(data);
        }
    }
}