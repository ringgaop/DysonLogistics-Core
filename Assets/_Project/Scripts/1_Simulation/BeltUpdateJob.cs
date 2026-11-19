using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// Parallel job to update item progress on belts.
    /// </summary>
    [BurstCompile]
    public partial struct BeltUpdateJob : IJobEntity
    {
        public float DeltaTime;

        // Update logic executed on multiple threads
        [BurstCompile]
        private void Execute(ref BeltSegment belt, ref DynamicBuffer<BeltItemBuffer> items)
        {
            // TODO: Implement Ring Buffer push/pop logic here.
            // TODO: Calculate speed modifier based on belt tier.
            
            float speed = 5.0f; // Configurable
            float deltaProgress = (speed * DeltaTime) / belt.Length;

            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i].Item;
                
                if (item.Progress < 1.0f)
                {
                    item.Progress += deltaProgress;
                    
                    // Clamp to end of belt, awaiting transfer to next belt
                    if (item.Progress > 1.0f) 
                    {
                        item.Progress = 1.0f;
                    }
                    
                    items[i] = new BeltItemBuffer { Item = item };
                }
            }
        }
    }
}
