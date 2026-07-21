using System.Collections.Generic;
using Enum;
using Player;
using RunTimeData;
using UI;
using UnityEngine;

namespace DataProvider
{
    public class StatUIProvider : MonoBehaviour
    {
        [SerializeField] private PlayerStatDataLister playerStatDataLister;

        private readonly Dictionary<StatType, StatBinding> _bindings = new();
        private bool _initialized;

        private void Start()
        {
            if (playerStatDataLister == null)
            {
                Debug.LogError("PlayerStatDataLister is not assigned.", this);
                enabled = false;
                return;
            }

            BuildBindings();
            Subscribe();
            _initialized = true;
        }

        private void OnEnable()
        {
            if (_initialized)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_initialized)
                Unsubscribe();
        }

        private void BuildBindings()
        {
            foreach (var provider in playerStatDataLister.DataList)
            {
                if (provider.StatData == null) continue;
                if (!playerStatDataLister.TryGetRuntimeStat(provider.StatData.statType, out RunTimeStat stat))
                    continue;

                StatUI view = CreateStatView();
                view.SetType(provider.StatData.statType);
                view.SetText(stat.Value.Value, stat.Value.Value);

                _bindings.TryAdd(provider.StatData.statType, new StatBinding(stat, view));
            }
        }

        private StatUI CreateStatView()
        {
            GameObject textObject = new("Stat Text");
            textObject.transform.SetParent(transform, false);
            return textObject.AddComponent<StatUI>();
        }

        private void Subscribe()
        {
            foreach (StatBinding binding in _bindings.Values)
                binding.Stat.Value.OnValueChanged += binding.View.SetText;
        }

        private void Unsubscribe()
        {
            foreach (StatBinding binding in _bindings.Values)
                binding.Stat.Value.OnValueChanged -= binding.View.SetText;
        }

        private readonly struct StatBinding
        {
            public RunTimeStat Stat { get; }
            public StatUI View { get; }

            public StatBinding(RunTimeStat stat, StatUI view)
            {
                Stat = stat;
                View = view;
            }
        }
    }
}
