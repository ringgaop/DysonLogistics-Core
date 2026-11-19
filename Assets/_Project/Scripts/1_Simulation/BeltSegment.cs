using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// Core data structure representing a conveyor belt segment.
    /// </summary>
    public struct BeltSegment : IComponentData
    {
        public float3 StartPos;
        public float3 EndPos;
        public float Length;
        
        // Ring Buffer Implementation for Items
        public int Capacity;
        public int HeadIndex;
        public int TailIndex;
    }

    /// <summary>
    /// Buffer containing the actual items on a specific belt.
    /// </summary>
    [InternalBufferCapacity(16)]
    public struct BeltItemBuffer : IBufferElementData
    {
        public ItemEntity Item;
    }
}
