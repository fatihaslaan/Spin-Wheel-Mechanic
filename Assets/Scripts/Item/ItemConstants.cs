using System.Collections.Generic;

namespace Item
{
    public static class ItemConstants
    {
        public const string SO_ITEM_DATA_ = "SO_Item_Data_";
        public const string ITEM = "Item";
        public static readonly Dictionary<ItemRarity, float> RARITY_CHANCES = new Dictionary<ItemRarity, float>
        {
            { ItemRarity.Common, 40f },
            { ItemRarity.Uncommon, 30f },
            { ItemRarity.Rare, 15f },
            { ItemRarity.Epic, 10f },
            { ItemRarity.Legendary, 5f },
        };

        public const string EO_POPUP_ITEM_DATA = "EO_Popup_Item_Data";
        public const string CANVAS_ITEM_POPUP = "Canvas_Item_Popup";
        public const string IMAGE_ITEM_BACKGROUND_ = "Image_Item_Backround_";
    }
}