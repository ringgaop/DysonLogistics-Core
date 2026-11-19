using Unity.Entities;
using Unity.Mathematics;

namespace DysonLogistics.Simulation.EnergyGrid
{
    /// <summary>
    /// 电网节点（电线杆/电力塔）
    /// </summary>
    public struct PowerNode : IComponentData
    {
        public int NetworkID; // 属于哪个电网 (由拓扑计算得出)
        public float CoverageRadius;
    }

    /// <summary>
    /// 发电机组件
    /// </summary>
    public struct PowerProducer : IComponentData
    {
        public float PowerOutput; // 恒定发电量
    }

    /// <summary>
    /// 耗电组件（机器、矿机等）
    /// </summary>
    public struct PowerConsumer : IComponentData
    {
        public float PowerDemand;
        public float CurrentEfficiency; // 电网给的效率 (0~1)
    }

    /// <summary>
    /// 电网共享数据（每个子网一份）
    /// </summary>
    public struct PowerNetworkData : IComponentData
    {
        public float TotalProduction;
        public float TotalConsumption;
        public float Efficiency;
    }
}
