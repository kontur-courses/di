using System;
using System.Collections.Generic;

public class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
{
    public int Compare(T obj1, T obj2)
    {
        if (obj1 == null || obj2 == null) throw new ArgumentException();
        return -(obj1.CompareTo(obj2));
    }
}
