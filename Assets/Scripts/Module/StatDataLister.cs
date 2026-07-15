using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Module
{
    public class StatDataLister : MonoBehaviour , IModule , IDataLister
    {
        public List<StatDataProvider> DataList { get; private set; } = new();
        private void Awake()
        {
            DataList.AddRange(GetComponentsInChildren<StatDataProvider>());
        }
    }
}