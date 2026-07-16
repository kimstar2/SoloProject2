using CoreLib;
using Enum;
using SO;

namespace RunTimeData
{
    public sealed class RunTimeStat
    {
        public StatType StatType;
        public float OriginValue;
        public NotifyValue<float> Value { get; } = new();

        public RunTimeStat(StatDataSo origin)
        {
            StatType = origin.statType;
            OriginValue = origin.statValue;
            Value.Value = origin.statValue;
        }

        public void Add(float value) => Value.Value += value;
        public void Multiply(float value) => Value.Value *= value;
    }
}