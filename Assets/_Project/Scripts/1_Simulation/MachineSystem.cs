using Unity.Burst;
using Unity.Entities;

namespace DysonLogistics.Simulation.Production
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct MachineSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new MachineProcessJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };

            // 并行调度所有的机器加工逻辑
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
}
