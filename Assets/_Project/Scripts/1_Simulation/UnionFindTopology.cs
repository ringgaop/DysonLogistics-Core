using Unity.Collections;
using Unity.Entities;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// High-performance Union-Find (Disjoint Set) with Path Compression and Union by Rank.
    /// Used to dynamically track connected conveyor belts and power grids without O(N) traversal.
    /// </summary>
    public struct TopologyGraph
    {
        private NativeArray<int> _parent;
        private NativeArray<int> _rank;

        public TopologyGraph(int size, Allocator allocator)
        {
            _parent = new NativeArray<int>(size, allocator);
            _rank = new NativeArray<int>(size, allocator);
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
                _rank[i] = 0;
            }
        }

        public void Dispose()
        {
            if (_parent.IsCreated) _parent.Dispose();
            if (_rank.IsCreated) _rank.Dispose();
        }

        /// <summary>
        /// Find with Path Compression. Amortized O(alpha(n)) ~ O(1).
        /// </summary>
        public int Find(int i)
        {
            if (_parent[i] == i) return i;
            // Path compression
            _parent[i] = Find(_parent[i]);
            return _parent[i];
        }

        /// <summary>
        /// Union by Rank.
        /// </summary>
        public void Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);

            if (rootI != rootJ)
            {
                if (_rank[rootI] < _rank[rootJ])
                {
                    _parent[rootI] = rootJ;
                }
                else if (_rank[rootI] > _rank[rootJ])
                {
                    _parent[rootJ] = rootI;
                }
                else
                {
                    _parent[rootJ] = rootI;
                    _rank[rootI]++;
                }
            }
        }
        
        public bool IsConnected(int i, int j) => Find(i) == Find(j);
    }
}
