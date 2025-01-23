using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private uint _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private uint _minDropValue;
    [SerializeField] private uint _maxDropValue;
    [SerializeField] private ItemRarity _rarity;

    public uint ID { get { return _id; } }
    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }

    public uint MinDropValue { get { return _minDropValue; } }
    public uint MaxDropValue { get { return _maxDropValue; } }
    public ItemRarity Rarity { get { return _rarity; } }
}