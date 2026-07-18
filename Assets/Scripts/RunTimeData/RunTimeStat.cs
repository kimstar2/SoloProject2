using CoreLib;
using Enum;
using SO;

namespace RunTimeData
{
    public sealed class RunTimeStat
    {
        public StatType StatType { get; }
        public float OriginValue { get; }
        public NotifyValue<float> Value { get; }

        public RunTimeStat(StatDataSo origin)
        {
            StatType = origin.statType;
            OriginValue = origin.statValue;
            Value = new NotifyValue<float>(origin.statValue);
        }

        public void Add(float value) => Value.Value += value;
        public void Multiply(float value) => Value.Value *= value;
        public void Apply(float addValue , float multiValue) => Value.Value = (Value.Value + addValue) * multiValue;
    }
}