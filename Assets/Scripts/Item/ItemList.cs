using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [Serializable]
    public struct ItemList
    {
        [SerializeField] private List<EarnableItem> _items;
        public List<EarnableItem> Items { get { return _items; } }
    }
}