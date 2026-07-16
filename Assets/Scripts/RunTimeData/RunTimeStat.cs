using Enum;
using SO;

namespace RunTimeData
{
    public sealed class RunTimeStat
    {
        public StatType StatType { get; }
        public float OriginValue { get; }
        public float Value { get; private set; }

        public RunTimeStat(StatDataSo origin)
        {
            StatType = origin.statType;
            OriginValue = origin.statValue;
            Value = origin.statValue;
        }
    }
}