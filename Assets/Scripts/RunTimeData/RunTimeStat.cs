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

        private float _permanentAdd;
        private float _permanentMultiplier = 1f;

        private float _deckAdd;
        private float _deckMultiplier = 1f;
        public float PermanentValue => (OriginValue + _permanentAdd) * _permanentMultiplier;

        public RunTimeStat(StatDataSo origin)
        {
            StatType = origin.statType;
            OriginValue = origin.statValue;

            Value = new NotifyValue<float>(OriginValue);
        }

        public void ApplyPermanentModifier(float add,float multiplier)
        {
            _permanentAdd += add;
            _permanentMultiplier *= multiplier;

            RefreshValue();
        }

        public void SetDeckModifiers(float totalAdd, float totalMultiplier)
        {
            _deckAdd = totalAdd;
            _deckMultiplier = totalMultiplier;

            RefreshValue();
        }

        public void ClearDeckModifiers()
        {
            _deckAdd = 0f;
            _deckMultiplier = 1f;

            RefreshValue();
        }

        private void RefreshValue()
        {
            Value.Value =
                (OriginValue + _permanentAdd + _deckAdd)
                * _permanentMultiplier * _deckMultiplier;
        }
    }
}