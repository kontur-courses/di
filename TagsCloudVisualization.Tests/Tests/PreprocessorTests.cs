using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests.Tests;

[TestFixture]
public class PreprocessorTests
{
    private readonly string projectDirectory 
        = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

    private readonly string testWordsPath;
    private readonly string[] words;

    public PreprocessorTests()
    {
        testWordsPath = string.Concat(projectDirectory, @"\testWords.txt");
        
        FileGenerator.Generate(testWordsPath, TestWords.RussianWordsWithUnnecessary,100);

        words = File.ReadAllLines(testWordsPath);
    }

    [Test]
    public void Process_SuccessPath_ShouldReturnListWordsInLowerFormat()
    {
        var resultWords = Preprocessor.Process(words).ToList();

        foreach (var word in resultWords)
        {
            Assert.IsTrue(word.All(x => !char.IsLetter(x) || char.IsLower(x)));
        }
    }
    
    [Test]
    public void Process_SuccessPath_ShouldReturnListWordsWithoutUnnecessaryWords()
    {
        var resultWords = Preprocessor.Process(words).ToList();

        foreach (var word in resultWords)
        {
            Assert.IsFalse(TestWords.UnnecessaryRussianWords.Contains(word));
        }
    }
}