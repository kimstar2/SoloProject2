using System.Collections.Generic;
using UnityEngine;

namespace UI.Card
{
    public class ApplyCard : MonoBehaviour // Test
    {
        [SerializeField] List<GameObject> cardPrefabs;
        [SerializeField] Transform target;
        private readonly Stack<GameObject> _crtCardStack = new();
        
        public void Add()
        {
            _crtCardStack.Push(Instantiate(cardPrefabs[Random.Range(0,cardPrefabs.Count)], target));
        }

        public void Remove()
        {
            if (_crtCardStack.Count < 1) return;
            Destroy(_crtCardStack.Pop());
        }
    }
}