using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private int _minDropValue;
    [SerializeField] private int _maxDropValue;
    [SerializeField] private ItemRarity _rarity;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }

    public int MinDropValue { get { return _minDropValue; } }
    public int MaxDropValue { get { return _maxDropValue; } }
    public ItemRarity Rarity { get { return _rarity; } }
}