﻿//
// File: Test_BtreeLinq.cs
//
// Exercise some of the LINQ API derived from Enumerable. This is a partial sample and only
// Last() is required for coverage testing.
//

using Kw.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_KwData
{
    public partial class Test_Btree
    {
        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void Test_LastE1 ()
        {
            Setup ();

            // Exception thrown when empty.
            var kv = tree2.Last ();
        }


        [TestMethod]
        public void Test_Last ()
        {
            Setup ();
            tree1.Add (3, -33);
            tree1.Add (1, -11);
            tree1.Add (2, -22);

            var kv = tree1.Last ();

            Assert.AreEqual (3, kv.Key, "didn't get expected last key");
            Assert.AreEqual (-33, kv.Value, "didn't get expected last value");
        }


        [TestMethod]
        public void Test_LinqAny ()
        {
            Setup ();

            var x1 = tree1.Any ();

            tree1.Add (1, 10);
            tree1.Add (3, 30);
            tree1.Add (2, 20);

            var x2 = tree1.Any ();

            Assert.IsFalse (x1);
            Assert.IsTrue (x2);
        }


        [TestMethod]
        public void Test_LongCount ()
        {
            Setup ();
            tree1.Add (3, -33);
            tree1.Add (1, -11);
            tree1.Add (2, -22);

            var result = tree1.LongCount ();
            var type = result.GetType ();

            Assert.AreEqual (3, result);
            Assert.AreEqual ("Int64", type.Name);
        }
    }
}