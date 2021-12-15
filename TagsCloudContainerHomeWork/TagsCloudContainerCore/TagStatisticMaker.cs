using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore;

public class TagStatisticMaker : IStatisticMaker
{
    private readonly IDictionary<string, int> tagsWithCount = new Dictionary<string, int>();

    private IList<KeyValuePair<string, int>> orderedPairs;

    private bool isChangeTags;

    private readonly Func<string, bool> tagFilter;

    public TagStatisticMaker(Func<string, bool> tagFilter = null)
    {
        tagFilter ??= _ => true;
        this.tagFilter = tagFilter;
    }

    public IEnumerable<KeyValuePair<string, int>> CountedTags => tagsWithCount;

    public KeyValuePair<string, int> GetMaxTag()
    {
        // ReSharper disable once InvertIf
        if (isChangeTags)
        {
            orderedPairs = tagsWithCount.OrderBy(x => x.Value).ToList();
            isChangeTags = false;
        }

        return orderedPairs[^1];
    }

    public KeyValuePair<string, int> GetMinTag()
    {
        // ReSharper disable once InvertIf
        if (isChangeTags)
        {
            orderedPairs = tagsWithCount.OrderBy(x => x.Value).ToList();
            isChangeTags = false;
        }

        return orderedPairs[0];
    }

    public void AddTags(IEnumerable<string> tags)
    {
        foreach (var tag in tags)
        {
            AddTag(tag);
        }
    }

    private void AddTag(string tag)
    {
        tag = tag.ToLowerInvariant();
        if (!tagFilter(tag))
        {
            return;
        }

        isChangeTags = true;

        if (!tagsWithCount.ContainsKey(tag))
        {
            tagsWithCount[tag] = 0;
        }

        tagsWithCount[tag]++;
    }
}