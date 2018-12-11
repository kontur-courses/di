using System.Linq;

namespace TagCloudCreation
{
    public class VerbRemover : PosRemover
    {
        public override string PrepareWord(string word, TagCloudCreationOptions options) =>
            PrepareWord(word, w => w.Split('_')
                                    .Last()
                                    .StartsWith("V"));
    }
}
