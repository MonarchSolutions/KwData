# KwData

Copyright © 2009-2012 Kasey Osborn (Kasewick@gmail.com)

License: Ms-PL

This library contains the BtreeDictionary class which implements a B+ tree data structure and is a functional equivalent of Microsoft's generic SortedDictionary class (a binary tree) but with improved performance characteristics in several areas. Written in C#.

## Overview

The Kw.Data namespace defined by this project currently contains just one class: BtreeDictionary. This class is a functional superset of the System.Collections.Generic.SortedDictionary class and may be substituted wherever a SortedDictionary is used. Two iterators are provided to support optimized range queries: SkipUntilKey(TKey) and BetweenKeys(TKey,TKey). Also included is an optimized Last() method.

Like SortedDictionary, BtreeDictionary space is guaranteed to stay within O(n) for any number of insertions and deletions, where n is the size of the collection. Worst case time is O(log n) for a search, insert, or delete operation for any number of insertions and deletions.

Performance is improved in many areas by using a B+ tree due to the clustering of data leading to fewer traversal operations and more hits on cache memory. Garbage collection activities should be reduced due to fewer allocations and the reuse of space. Sequential operations are particularly improved. Iteration over the entire collection will see times less than half that of SortedDictionary.

## Implementation details

The BCL SortedDictionary class is well tuned, compact, and very fast for relatively small amounts of data. However for large amounts of data such as millions of items, its tree height becomes quite large and it makes a very large number of allocations. This has an obvious impact on performance.

BtreeDictionary implements a B+ tree structure with only child and right sibling pointers. All nodes maintain 50% fill except the rightmost node of any level.

BtreeDictionary implements a non-lazy delete algorithm. Typical B tree implementations use lazy deleting where nodes are not required to maintain 50% fill once any deletion has taken place. Lazy deletion is easy to program. While lazy deletes may be desired for disk-based B+ trees to reduce contention, it is not desireable for in-memory implementations as it degrades time and space complexities.

Disclaimer: While there are no known bugs, the BtreeDictionary class has probably not seen the degree of testing that the SortedDictionary class has. Use at your own risk.
