using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TopologyTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EmptyMap()
        {
            int[] map = null;
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(0, result);
        }

       [TestMethod]
       public void SingleMapEntry()
        {
            int[] map = { 1 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void IncreasingMapOnly()
        {
            int[] map = { 4, 7, 8, 9, 11, 15, 17 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void IncreasingMapOnlyWithPlateau()
        {
            int[] map = { 4, 7, 8, 9, 9, 9, 9, 11, 15, 15, 15, 17 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void IncreasingMapOnlyWithNegativeValues()
        {
            int[] map = { -5, -4,  -3, 7, 8, 9 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void OnePeekOneValley()
        {
            int[] map = { 3, 7, 8, 9, 8 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(2, result);
        }

        [TestMethod]
        public void TwoPeekOneValley()
        {
            int[] map = { 3, 7, 8, 9, 8, 6, 6, 7 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(3, result);
        }

        [TestMethod]
        public void TwoPeekTwoValley()
        {
            int[] map = { 3, 7, 8, 9, 8, 6, 6, 7, 8, 9, 9, 10, 10, 10, 8, -1 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(4, result);
        }

        [TestMethod]
        public void TwoPeekTwoValley2()
        {
            int[] map = { -5, -5, -4, -3, -3, 1, -2, -3, -4, -4, -3, -2, -10 };
            int result = Topology.Map.Castles(map);
            Assert.AreEqual<int>(4, result);
        }
    }
}
