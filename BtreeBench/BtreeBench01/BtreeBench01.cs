﻿//
// Program: BtreeBench01.cs
// Purpose: Benchmark SortedDictionary and BtreeDictionary comparisons with range query narrative.
//
// Usage notes:
// - For valid results, run Release build outside Visual Studio.
// - Adjust reps to change test duration.  Higher values show greater BtreeDictionary improvements.
//

using Kw.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace KwDataBench
{
    class BtreeBench01
    {
        static void Main ()
        {
            int reps = 5000000;

            Console.WriteLine ("LINQ's Last extension method will enumerate over the *entire* collection giving");
            Console.WriteLine ("a time complexity of O(n) regardless of the data structure.  This is due to the");
            Console.WriteLine ("\"one size fits all\" approach of LINQ.  SortedDictionary supplies no optimized");
            Console.WriteLine ("implementation of Last. Also, SortedDictionary does not supply any optimized");
            Console.WriteLine ("way of performing queries based on a key range.  Again, the time complexity of");
            Console.WriteLine ("such an operation is O(n).\n");

            var sd = new SortedDictionary<int, int> ();

            Console.Write ("Loading SortedDictionary with " + reps + " elements:\n\nLoad time = ");

            Stopwatch watch1 = new Stopwatch ();
            watch1.Reset ();
            watch1.Start ();

            for (int i = 0; i < reps; ++i)
                sd.Add (i, -i);

            var time11 = watch1.ElapsedMilliseconds;

            var last1 = sd.Last ();

            var time12 = watch1.ElapsedMilliseconds;

            Console.WriteLine (time11 + "ms");
            Console.WriteLine ("Last time = " + (time12 - time11) + "ms");

            ////

            Console.WriteLine ("\nBtreeDictionary has its own implementation of Last() which does not suffer the");
            Console.WriteLine ("performance hit that SortedDictionary.Last() does.  BtreeDictionary also");
            Console.WriteLine ("supports optimized range queries with its SkipUntilKey(TKey) and");
            Console.WriteLine ("BetweenKeys(TKey,TKey) methods.\n");

            var bt = new BtreeDictionary<int, int> ();

            Console.Write ("Loading BtreeDictionary with " + reps + " elements:\n\nLoad time = ");

            Stopwatch watch2 = new Stopwatch ();
            watch2.Reset ();
            watch2.Start ();

            for (int i = 0; i < reps; ++i)
                bt.Add (i, -i);

            var time21 = watch2.ElapsedMilliseconds;

            var lastKV = bt.Last ();

            var time22 = watch2.ElapsedMilliseconds;

            // Range query: Sum the middle 100 values.
            var rangeVals = bt.BetweenKeys (reps/2-50, reps/2+50).Sum (x => x.Value);

            var time23 = watch2.ElapsedMilliseconds;

            Console.WriteLine (time21 + "ms");
            Console.WriteLine ("Last time = " + (time22 - time21) + "ms");
            Console.WriteLine ("Range time = " + (time23 - time22) + "ms");
        }
    }
}
