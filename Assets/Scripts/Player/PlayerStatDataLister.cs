using System.Collections.Generic;
using Enum;
using Interface;
using Interface.Marker;
using RunTimeData;
using SO;
using SO.Event;
using SO.Event.Func;
using UnityEngine;

namespace Player
{
    public class PlayerStatDataLister : MonoBehaviour, IModule
    {
        [field:SerializeField] public EventChannel DeckChangedChannel { get; private set; }
        [field:SerializeField] public StatModifierQueryChannel ModifierQueryChannel { get; private set; }
        
        private readonly List<IStatDataProvider> _originDataList = new();
        public IReadOnlyList<IStatDataProvider> DataList => _originDataList;

        private readonly Dictionary<StatType, RunTimeStat> _runtimeStatMap = new();

        private void Awake()
        {
            _originDataList.Clear();
            _originDataList.AddRange(GetComponentsInChildren<IStatDataProvider>());

            foreach (IStatDataProvider provider in DataList)
            {
                StatDataSo originData = provider.StatData;
                if (originData == null) continue;

                if (!_runtimeStatMap.TryAdd(originData.statType, new RunTimeStat(originData)))
                {
                    Debug.LogError(
                        $"Duplicate stat type {originData.statType} under {gameObject.name}.",
                        this);
                }
            }
        }

        private void Start()
        {
            RefreshDeckModifiers();
        }

        public bool ApplyPermanentCard(ICard card)
        {
            if (card?.CardDefinition == null) return false;

            bool applied = false;

            foreach (CardDataSo modifier in card.CardDefinition.StatModifiers)
            {
                if (modifier == null) continue;
                if (!_runtimeStatMap.TryGetValue(modifier.statType, out RunTimeStat runtimeStat)) continue;

                runtimeStat.ApplyPermanentModifier(modifier.statAddValue, modifier.statMultiValue);
                applied = true;
            }
            return applied;
        }

        public bool CanApplyCard(ICard card)
        {
            if (card?.CardDefinition == null) return false;

            foreach (CardDataSo modifier in card.CardDefinition.StatModifiers)
            {
                if (modifier != null && _runtimeStatMap.ContainsKey(modifier.statType))
                    return true;
            }

            return false;
        }

        public bool TryGetRuntimeStat(StatType statType, out RunTimeStat runtimeStat)
        {
            return _runtimeStatMap.TryGetValue(statType, out runtimeStat);
        }
        
        public void RefreshDeckModifiers()
        {
            IEnumerable<CardDataSo> modifiers = ModifierQueryChannel?.RaiseEvent();
            RecalculateDeckModifiers(modifiers);
        }
        
        private void RecalculateDeckModifiers(IEnumerable<CardDataSo> modifiers)
        {
            Dictionary<StatType, float> addTotals = new();
            Dictionary<StatType, float> multiplierTotals = new();

            if (modifiers != null)
            {
                foreach (CardDataSo modifier in modifiers)
                {
                    if (modifier == null) continue;
                    
                    StatType statType = modifier.statType;
                    if (!_runtimeStatMap.ContainsKey(statType)) continue;

                    addTotals.TryAdd(statType, 0f);
                    multiplierTotals.TryAdd(statType, 1f);

                    addTotals[statType] += modifier.statAddValue;
                    multiplierTotals[statType] *= modifier.statMultiValue;
                }
            }

            foreach (var pair in _runtimeStatMap)
            {
                StatType statType = pair.Key;
                RunTimeStat runtimeStat = pair.Value;

                float totalAdd = addTotals.GetValueOrDefault(statType, 0f);
                float totalMultiplier = multiplierTotals.GetValueOrDefault(statType, 1f);

                runtimeStat.SetDeckModifiers(totalAdd, totalMultiplier);
            }
        }
    }
}
