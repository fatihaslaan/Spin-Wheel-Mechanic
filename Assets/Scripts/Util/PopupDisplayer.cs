using Core;
using Item;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Util
{
    public class PopupDisplayer
    {
        public static void ShowItemPopup(string header, string message, List<EarnableItem> items, string firstButtonText = null, Action firstItemAction = null, string secondButtonText = null, Action secondButtonAction = null)
        {
            Addressables.LoadAssetAsync<GameObject>(ItemConstants.CANVAS_ITEM_POPUP).Completed += (handle) =>
            {
                ItemPopupController _popupController = GameObject.Instantiate(handle.Result as GameObject).GetComponent<ItemPopupController>();

                _popupController.AddItemsToPanel(items);
                SetBasePopup(handle, _popupController, header, message, firstButtonText, firstItemAction, secondButtonText, secondButtonAction);
            };
        }

        private static void SetBasePopup(AsyncOperationHandle asyncOperation, BasePopupController popupController, string header, string message, string firstButtonText = null, Action firstItemAction = null, string secondButtonText = null, Action secondButtonAction = null)
        {
            popupController.SetTexts(header, message);

            popupController.AddListenerToFirstButton(() => popupController.ClosePopup(asyncOperation)); //Close Popup After action
            popupController.AddListenerToSecondButton(() => popupController.ClosePopup(asyncOperation));

            if (firstItemAction == null && secondButtonAction == null)
            {
                popupController.CloseSecondButton(); //Close Second Button And Leave First One For Closing Action
                popupController.SetFirstButtonText("Close");
            }
            if (firstItemAction != null)
            {
                popupController.AddListenerToFirstButton(() => firstItemAction?.Invoke());
                popupController.SetFirstButtonText(firstButtonText);
                if (secondButtonAction == null)
                    popupController.CloseSecondButton();
            }
            if (secondButtonAction != null)
            {
                popupController.AddListenerToSecondButton(() => secondButtonAction?.Invoke());
                popupController.SetSecondtButtonText(secondButtonText);
                if (firstItemAction == null)
                    popupController.CloseFirstButton();
            }
        }
    }
}