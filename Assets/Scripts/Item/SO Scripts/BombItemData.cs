using Item;
using UnityEngine;

namespace WheelMechanic
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Bomb Item Data")]
    public class BombItemData : ItemData
    {
        public override void AddToInventory<WheelManager>(WheelManager wheel)
        {
            wheel.ClearInventory();
        }
    }
}