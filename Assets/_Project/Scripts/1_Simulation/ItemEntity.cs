using Unity.Entities;
using Unity.Mathematics;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// Represents an item moving on a belt. Pure struct to ensure cache friendliness.
    /// </summary>
    public struct ItemEntity : IComponentData
    {
        public int ItemID;
        // Parametric position on the belt spline (0.0 to 1.0)
        public float Progress; 
        // Cached belt entity reference for O(1) lookup
        public Entity CurrentBelt; 
    }
}
