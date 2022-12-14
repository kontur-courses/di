using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.MorphAnalyzer;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CustomWordsFilterTests
{
    private IWordsFilter _wordsFilter;

    [SetUp]
    public void Setup()
    {
        var mock = new Mock<IMorphAnalyzer>();
        mock.Setup(a => a.GetWordsMorphInfo(It.IsAny<IEnumerable<string>>()))
            .Returns(new Func<IEnumerable<string>, Dictionary<string, WordMorphInfo>>((callInfo) => new Dictionary<string, WordMorphInfo>()));

        _wordsFilter = new CustomWordsFilter(mock.Object);
    }

    [Test]
    public void FilterWords_EmptyOptions_ReturnAllWords()
    {
        var options = new VisualizationOptions()
        {
        };
        // options.BoringWords.options.ExcludedPartsOfSpeech


        var wordsAndCount = new Dictionary<string, int>()
        {
            {
                "test1", 1
            },
            {
                "test2", 2
            },
            {
                "test3", 3
            },
        };
        var actualWords = _wordsFilter.FilterWords(wordsAndCount, options);
        actualWords.Should().BeEquivalentTo(wordsAndCount);
    }

    [Test]
    public void FilterWords_PartsOfSpeechInOptions_ExcludePartsOfSpeech()
    {
        var mock = new Mock<IMorphAnalyzer>();
        mock.Setup(a => a.GetWordsMorphInfo(It.IsAny<IEnumerable<string>>()))
            .Returns(new Func<IEnumerable<string>, Dictionary<string, WordMorphInfo>>((callInfo) =>
                new Dictionary<string, WordMorphInfo>()
                {
                    {
                        "test1", new WordMorphInfo()
                        {
                            PartsOfSpeech = new List<string>()
                            {
                                "ADV"
                            }
                        }
                    },
                    {
                        "test2", new WordMorphInfo()
                        {
                            PartsOfSpeech = new List<string>()
                            {
                                "PART"
                            }
                        }
                    },
                    {
                        "test3", new WordMorphInfo()
                        {
                            PartsOfSpeech = new List<string>()
                            {
                                "CONJ"
                            }
                        }
                    },
                    {
                        "test4", new WordMorphInfo()
                        {
                            PartsOfSpeech = new List<string>()
                            {
                                "PR"
                            }
                        }
                    },
                }
            ));

        _wordsFilter = new CustomWordsFilter(mock.Object);

        var options = new VisualizationOptions()
        {
            ExcludedPartsOfSpeech = new List<string>()
            {
                "PART",
                "CONJ"
            }
        };

        var wordsAndCount = new Dictionary<string, int>()
        {
            {
                "test1", 1
            },
            {
                "test2", 2
            },
            {
                "test3", 3
            },
            {
                "test4", 3
            },
        };

        var expectedWords = new Dictionary<string, int>()
        {
            {
                "test1", 1
            },
            {
                "test4", 3
            }
        };
        var actualWords = _wordsFilter.FilterWords(wordsAndCount, options);
    }

    [Test]
    public void FilterWords_BoringWords_ExcludeBoringWords()
    {
        var options = new VisualizationOptions()
        {
            BoringWords = new List<string>()
            {
                "test1",
                "test4"
            }
        };
        // options.BoringWords.options.ExcludedPartsOfSpeech


        var wordsAndCount = new Dictionary<string, int>()
        {
            {
                "test1", 1
            },
            {
                "test2", 2
            },
            {
                "test3", 3
            },
            {
                "test4", 3
            },
        };

        var expectedWords = new Dictionary<string, int>()
        {
            {
                "test2", 2
            },
            {
                "test3", 3
            }
        };
        var actualWords = _wordsFilter.FilterWords(wordsAndCount, options);
        actualWords.Should().BeEquivalentTo(expectedWords);
    }
}