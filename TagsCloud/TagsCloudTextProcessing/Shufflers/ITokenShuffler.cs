using System.Collections.Generic;
 
 namespace TagsCloudTextProcessing.Shufflers
 {
     public interface ITokenShuffler
     {
         IEnumerable<Token> Shuffle(IEnumerable<Token> tokens);
     }
 }