using Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Item
{
    public class ItemPopupController : BasePopupController
    {
        [SerializeField] private ItemPanelController _itemPanelController;
        [SerializeField] private ScrollRect _scrollRect;

        public void AddItemsToPanel(List<EarnableItem> wheelItems)
        {
            if (wheelItems == null)
            {
                _scrollRect.gameObject.SetActive(false);
                return;
            }
            _scrollRect.gameObject.SetActive(true);
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
            if (_scrollRect == null)
            {
                ObjectFinder.FindObjectInChilderenWithType<ScrollRect>(ref _scrollRect, transform);
            }
        }
    }
}