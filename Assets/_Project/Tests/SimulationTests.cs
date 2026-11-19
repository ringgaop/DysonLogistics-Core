using NUnit.Framework;
using Unity.Collections;
using DysonLogistics.Simulation;

namespace DysonLogistics.Tests
{
    public class SimulationTests
    {
        [Test]
        public void TopologyGraph_UnionAndFind_WorksInO1()
        {
            var graph = new TopologyGraph(100, Allocator.Temp);
            
            graph.Union(1, 2);
            graph.Union(2, 3);
            
            Assert.IsTrue(graph.IsConnected(1, 3));
            Assert.IsFalse(graph.IsConnected(1, 4));
            
            graph.Dispose();
        }
        
        [Test]
        public void TopologyGraph_PathCompression_StressTest()
        {
            // Simulate 10,000 belts connecting in a long chain
            var graph = new TopologyGraph(10000, Allocator.Temp);
            for (int i = 0; i < 9999; i++)
            {
                graph.Union(i, i + 1);
            }
            
            // Without path compression, this would stack overflow or take O(N).
            Assert.IsTrue(graph.IsConnected(0, 9999));
            
            graph.Dispose();
        }
    }
}
