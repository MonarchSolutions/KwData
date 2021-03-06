﻿//
// File: TestBtree.cs
//
// Run BtreeDictionary's suite against SortedDictionary to demonstrate identical behavior
// between the two API's.  To perform baseline test against SortedDictionary:
//
// Solution Explorer -> BtreeTest -> Properties
// -> Build -> Conditional compilation symbols
//    -> input TEST_SORTEDDICTIONARY
//

using Kw.Data;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Test_KwData
{
    [TestClass]
    public partial class Test_Btree
    {
#if TEST_SORTEDDICTIONARY
        SortedDictionary<int, int> tree1;
        SortedDictionary<string, int> tree2;
#else
        BtreeDictionary<int, int> tree1;
        BtreeDictionary<string, int> tree2;
#endif
        ICollection<KeyValuePair<int, int>> genCol1;
        ICollection<KeyValuePair<string, int>> genCol2;
        ICollection<int> genKeys1;
        ICollection<string> genKeys2;
        ICollection<int> genValues2;

        System.Collections.IDictionary objCol1;
        System.Collections.IDictionary objCol2;


        // Must not contain value 50.
        static int[] keys = new int[] { 12, 28, 15, 18, 14, 19, 25 };

        public void Setup () { Setup (5); }

        public void Setup (int order)
        {
#if TEST_SORTEDDICTIONARY        
            tree1 = new SortedDictionary<int, int> ();
            tree2 = new SortedDictionary<string, int> ();
#else
            tree1 = new BtreeDictionary<int, int> (order);
            tree2 = new BtreeDictionary<string, int> (order);
#endif

            Type treeType = tree1.GetType ();

            // For testing explicit implementations.
            genCol1 = (ICollection<KeyValuePair<int, int>>) tree1;
            genCol2 = (ICollection<KeyValuePair<string, int>>) tree2;
            genKeys1 = (ICollection<int>) tree1.Keys;
            genKeys2 = (ICollection<string>) tree2.Keys;
            genValues2 = (ICollection<int>) tree2.Values;
            objCol1 = (System.Collections.IDictionary) tree1;
            objCol2 = (System.Collections.IDictionary) tree2;
        }
    }
}
