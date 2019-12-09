using System.Collections.Generic;
 
 namespace TagsCloudTextPreparation.Shufflers
 {
     public interface ITokenShuffler
     {
         IEnumerable<Token> Shuffle(IEnumerable<Token> tokens);
     }
 }