using Item;
using UnityEngine;

namespace WheelMechanic
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Reward Item Data")]
    public class WheelRewardItemData : ItemData
    {
        public override void AddToInventory<WheelManager>(WheelManager wheel)
        {
            wheel.ItemAddedToInventory();
        }
    }
}