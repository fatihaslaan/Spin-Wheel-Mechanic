using UnityEngine;

namespace Item
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private Items _item;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemRarity _rarity;

        //Don't need that if we use backend
        [SerializeField] private int _minDropValue;
        [SerializeField] private int _maxDropValue;
        [SerializeField] private bool _dontIncludeInWheel;

        public Items Item { get { return _item; } }
        public string Name { get { return _name; } }
        public Sprite Sprite { get { return _sprite; } }
        public ItemRarity Rarity { get { return _rarity; } }

        public int MinDropValue { get { return _minDropValue; } }
        public int MaxDropValue { get { return _maxDropValue; } }
        public bool DontIncludeInWheel { get { return _dontIncludeInWheel; } }

        public abstract void AddToInventory<T>(T t) where T : IInventoryManager;
    }
}