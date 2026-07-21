using System.Collections.Generic;
using UnityEngine;

namespace SO.Event.Func
{
    [CreateAssetMenu(menuName = "SO/Events/Func/Stat Modifier Query", order = 0)]
    public class StatModifierQueryChannel : AbstractFunc<IEnumerable<CardDataSo>> {}
}