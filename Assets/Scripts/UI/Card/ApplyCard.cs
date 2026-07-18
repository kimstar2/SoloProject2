using System.Collections.Generic;
using UnityEngine;

namespace UI.Card
{
    public class ApplyCard : MonoBehaviour // Test
    {
        [SerializeField] GameObject cardPrefab;
        [SerializeField] Transform target;
        Stack<GameObject> _crtCardStack = new();
        
        public void Add()
        {
            _crtCardStack.Push(Instantiate(cardPrefab, target));
        }

        public void Remove()
        {
            if (_crtCardStack.Count < 1) return;
            Destroy(_crtCardStack.Pop());
        }
    }
}