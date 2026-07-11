using DG.Tweening;
using UnityEngine;

namespace Interface
{
    public interface IDash
    {
        float DashMulti { get; }
        float DashDur { get; }
        float DashCool { get; }
        Ease DashEaseType { get; }
        bool IsDash { get; }
        
        void Dash();
    }
}