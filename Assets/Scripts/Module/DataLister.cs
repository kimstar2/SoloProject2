using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Module
{
    public class DataLister : MonoBehaviour , IModule , IDataLister
    {
        public List<IDataProvider> OriginDataList { get; private set; } = new();
        
        private void Awake()
        {
            OriginDataList.AddRange(GetComponentsInChildren<IDataProvider>());
        }
    }
}