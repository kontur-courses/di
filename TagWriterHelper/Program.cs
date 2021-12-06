using System;
using System.Collections.Generic;

namespace TagWriterHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helper - чтобы не заполнять файлы с тэгами вручную
            var path = "...";
            Writer.Write(path, GetTags());
        }

        static IEnumerable<Tag> GetTags()
        {
            //....
            yield break;
        }
    }
}
