using SO.CasterData;
using UnityEngine;

namespace Interface
{
    public interface ICastable
    {
        void HandleSetData(CasterDataSo data);
        void Cast(Collider2D target);
    }
}