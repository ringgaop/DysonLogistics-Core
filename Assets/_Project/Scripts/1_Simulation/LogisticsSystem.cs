using Unity.Burst;
using Unity.Entities;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// System orchestrator for the logistics simulation.
    /// </summary>
    [BurstCompile]
    public partial struct LogisticsSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new BeltUpdateJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            
            // Schedule the job to run in parallel across all available threads
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
}
