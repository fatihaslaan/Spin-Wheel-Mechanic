using UnityEngine;

namespace Item
{
    [System.Serializable]
    public class EarnableItem
    {
        [SerializeField] private Items _item;
        [SerializeField] private int _value;

        public Items Item { get { return _item; } }
        public int Value { get { return _value; } }

        public EarnableItem(Items item, int value)
        {
            _item = item;
            _value = value;
        }

        public void AddValue(int value)
        {
            _value += value;
        }
    }
}