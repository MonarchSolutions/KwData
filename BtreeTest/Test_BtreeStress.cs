using Kw.Data;
using Kw.Combinatorics;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_KwData
{
    public partial class Test_Btree
    {
        [TestMethod]
        public void Stress_With_Long_Permutations ()
        {
            for (int order = 5; order < 9; ++order)
            {
                Setup (order);

                for (int w = 1; w <= 500; ++w)
                {
                    for (int m = 0; m < w; m++)
                        tree1.Add (m, m + 1000);
                    for (int m = w - 1; m >= 0; m--)
                        tree1.Remove (m);

                    Assert.AreEqual (0, tree1.Count);
                }
            }
        }


        [TestMethod]
        public void Stress_With_Many_Permutations ()
        {
            Setup (5);

            for (int x = 1; x < 99; ++x)
            {
                tree1.Add (x, x + 1000);
            }

            tree1.Clear ();
            
            for (int w = 5; w < 7; ++w)
            foreach (Permutation tp in new Permutation (w).GetRows ())
            {
                Console.WriteLine (tp);
                for (int m = 0; m < tp.Width; ++m)
                    tree1.Add (tp[m], tp[m] + 100);

                tree1.Clear ();
            }
        }


        [TestMethod]
        public void Stress_Remove_For_Long_Branch_Shift ()
        {
            Setup (6);

            for (int size = 1; size < 90; ++size)
            {
                for (int i = 1; i <= size; ++i)
                    tree1.Add (i, i+200);

                for (int i = 1; i <= size; ++i)
                {
                    tree1.Remove (i);

#if (!TEST_SORTEDDICTIONARY && DEBUG)
                    tree1.SanityCheck ();
#endif
                }
            }
        }


        [TestMethod]
        public void Stress_Remove_Sliding_Window_For_Coalesce ()
        {
            Setup (5);

            for (int size = 65; size <= 75; ++size)
            {
                for (int a = 1; a <= size; ++a)
                    for (int b = a; b <= size; ++b)
                    {
                        tree1.Clear ();
                        for (int i = 1; i <= size; ++i)
                            tree1.Add (i, i + 100);

                        for (int i = a; i <= b; ++i)
                            tree1.Remove (i);

#if (!TEST_SORTEDDICTIONARY && DEBUG)
                        tree1.SanityCheck ();
#endif
                    }
            }
        }


        [TestMethod]
        public void Test_Remove_Span_For_Nontrivial_Coalesce ()
        {
            Setup ();

            for (int key = 1; key < 70; ++key)
                tree1.Add (key, key + 100);

            for (int key = 19; key <= 25; ++key)
                tree1.Remove (key);
        }


        [TestMethod]
        public void Test_Remove_Span_For_Branch_Balancing ()
        {
            Setup (6);

            for (int key = 1; key <= 46; ++key)
                tree1.Add (key, key + 100);

            for (int key = 1; key <= 46; ++key)
            {
                tree1.Remove (key);

#if (!TEST_SORTEDDICTIONARY && DEBUG)
                tree1.SanityCheck ();
#endif
            }
        }


        [TestMethod]
        public void Test_Add_For_Splits ()
        {
            Setup (5);

            for (int k = 0; k < 99; k += 8)
                tree1.Add (k, k + 100);

            tree1.Add (20, 1);
            tree1.Add (50, 1);
            tree1.Add (66, 132);
            tree1.Remove (20);
            tree1.Add (38, 147);
            tree1.Add (35, 142);
            tree1.Add (12, 142);
            tree1.Add (10, 147);
            tree1.Add (36, 147);
            tree1.Remove (12);
            tree1.Remove (8);
            tree1.Remove (10);
            tree1.Remove (88);

            tree1.Remove (56);
            tree1.Remove (80);
            tree1.Remove (96);
            tree1.Add (18, 118);
            tree1.Add (11, 111);

#if (!TEST_SORTEDDICTIONARY && DEBUG)
            // Assert: exceptions will be thrown if tree is invalid or underfilled.
            tree1.SanityCheck ();
#endif
        }
    }
}
