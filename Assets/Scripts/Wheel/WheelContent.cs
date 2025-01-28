using Item;

namespace WheelMechanic
{
    [System.Serializable]
    public class WheelContent
    {
        public EarnableItem[] wheelItems = new EarnableItem[WheelConstants.WHEEL_SLICE_COUNT];
    }
}