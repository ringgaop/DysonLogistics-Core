using Unity.Entities;
using Unity.Collections;

namespace DysonLogistics.Simulation.Production
{
    /// <summary>
    /// 配方数据结构，使用 BlobAsset 存储在内存中连续排列，极度 Cache 友好。
    /// </summary>
    public struct RecipeData
    {
        public int InputItemID;
        public int InputAmount;
        public int OutputItemID;
        public int OutputAmount;
        public float CraftTime;
    }

    /// <summary>
    /// 机器状态的枚举
    /// </summary>
    public enum MachineState : byte
    {
        Idle = 0,       // 空闲（缺料）
        Working = 1,    // 运行中
        Blocked = 2     // 堵塞（产物堆满）
    }

    /// <summary>
    /// 单个机器的 ECS 组件
    /// </summary>
    public struct AssemblerMachine : IComponentData
    {
        public BlobAssetReference<RecipeData> Recipe; // 引用配方
        public MachineState State;
        public float CurrentProgress; // 当前进度 (0 ~ CraftTime)
        
        // 内部库存（模拟爪子/传送带进出）
        public int InternalInputCount;
        public int InternalOutputCount;
    }
}
