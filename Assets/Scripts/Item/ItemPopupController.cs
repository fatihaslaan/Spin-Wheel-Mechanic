using Core;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Item
{
    public class ItemPopupController : BasePopupController
    {
        [SerializeField] private ItemPanelController _itemPanelController;

        public void AddItemsToPanel(List<EarnableItem> wheelItems)
        {
            if (wheelItems == null) return;
            foreach (EarnableItem item in wheelItems)
            {
                _itemPanelController.AddItemToPanel(item, ItemConstants.EO_POPUP_ITEM_DATA);
            }
        }

        protected override void OnOnValidate()
        {
            if (_itemPanelController == null)
            {
                ObjectFinder.FindObjectInChilderenWithType<ItemPanelController>(ref _itemPanelController, transform);
            }
        }
    }
}