using System;
using System.IO;
using Autofac;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.UI
{
    public class SetBoringTagsSelectorAction : ConsoleUiAction
    {
        public override string Category => "Preprocessors";
        public override string Name => "SetBoringTagsSelector";
        public override string Description { get; }

        public SetBoringTagsSelectorAction(TextWriter writer, TextReader reader, ContainerBuilder builder)
            : base(reader, writer, builder)
        {
        }

        public override void Perform()
        {
            writer.WriteLine("Enter selector to choose all 'good' tags");
            writer.WriteLine("Don`t forget to activate CustomTagsFilter!");
            var selector = reader.ReadLine();
            while (true)
            {
                try
                {
                    var t = CSharpScript.EvaluateAsync<CustomTagsFilter.RelevantTag>(selector);
                    t.Wait();
                    builder.RegisterInstance(t.Result)
                        .As<CustomTagsFilter.RelevantTag>();
                    return;
                }
                catch (Exception)
                {
                    writer.WriteLine("It wasn`t selector" +
                                     "It should be Func<SimpleTag, bool> like that:");
                    writer.WriteLine("t => t.Word.Length < 10");
                }
            }
        }
    }
}