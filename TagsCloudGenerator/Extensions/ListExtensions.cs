using System;
using System.Collections.Generic;

namespace TagsCloudGenerator
{
    public static class ListExtensions
    {
        public static List<T> ShuffleList<T>(this List<T> inputList)
        {
            var randomList = new List<T>(inputList.Count);

            var random = new Random();
            while (inputList.Count > 0)
            {
                var randomIndex = random.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }

            return randomList; 
        }
    }
}