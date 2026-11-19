using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DysonLogistics.Simulation.Production
{
    /// <summary>
    /// 机器加工逻辑的 Job，跨线程并行计算。
    /// 不使用任何面向对象，完全通过指针和结构体操作。
    /// </summary>
    [BurstCompile]
    public partial struct MachineProcessJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref AssemblerMachine machine)
        {
            // 如果产物满了，机器停止
            if (machine.InternalOutputCount >= machine.Recipe.Value.OutputAmount)
            {
                machine.State = MachineState.Blocked;
                return;
            }

            switch (machine.State)
            {
                case MachineState.Idle:
                    // 尝试抓取原料（实际项目中这里会和物流系统的 Buffer 交互）
                    if (machine.InternalInputCount >= machine.Recipe.Value.InputAmount)
                    {
                        machine.InternalInputCount -= machine.Recipe.Value.InputAmount;
                        machine.State = MachineState.Working;
                        machine.CurrentProgress = 0f;
                    }
                    break;

                case MachineState.Working:
                    machine.CurrentProgress += DeltaTime;
                    
                    if (machine.CurrentProgress >= machine.Recipe.Value.CraftTime)
                    {
                        // 生产完成，增加产物
                        machine.InternalOutputCount += machine.Recipe.Value.OutputAmount;
                        machine.CurrentProgress = 0f;
                        machine.State = MachineState.Idle;
                    }
                    break;
            }
        }
    }
}
