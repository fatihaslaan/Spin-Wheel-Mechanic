using Item;
using System.Collections.Generic;

namespace WheelMechanic
{    public static class WheelConstants
    {
        public const int WHEEL_SLICE_COUNT = 8;
        public const string EO_REWARD_ITEM_DATA = "EO_Reward_Item_Data"; //Reward Panel Item

        public const float CHANCE_MULTIPLIER_BY_EACH_STAGE = 1.1f;
        //Don't multiply theese rarities each stage
        public static readonly List<ItemRarity> EXCLUDED_FROM_MULTIPLIER = new List<ItemRarity> { ItemRarity.Common };

        public const string BOMB_EXPLODE_TITLE = "Oh No A Bomb Exploded";
        public const string BOMB_EXPLODE_MESSAGE = "You Will Lose All Items If You Don't Pay To Continue";
        public const string NO_MONEY_TITLE = "No Money";
        public const string NO_MONEY_MESSAGE = "You Don't Have Enough Money";
        public const string GAME_OVER_TITLE = "Game Over You Won";
        public const string GAME_OVER_MESSAGE = "You Won These Items! Don't Worry You Can Win Again!";
        public const string RESTART = "Restart";
        public const string CONTINUE = "Continue";
        public const string BUTTON_SPECIAL_STAGE = "Button_Special_Stage";
        public const string FREE_ITEM_FROM_STAGE_TITLE = "Free Items";
        public const string FREE_ITEM_EARNED_FROM_STAGE_MESSAGE = "You Got Free Items From Special Stage";
        public const string FREE_ITEM_FROM_STAGE_MESSAGE = "You Will Earn These Items On This Stage";

        public const float WHEEL_SPIN_TIME = 2f;

        public const int CONTINUE_COST = 100;
    }
}