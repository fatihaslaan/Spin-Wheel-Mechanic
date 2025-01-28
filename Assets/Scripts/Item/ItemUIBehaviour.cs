using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Item
{
    public class ItemUIBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemText;
        [SerializeField] private Image _itemImage;

        private EarnableItem _earnableItem;
        public EarnableItem EarnableItem { get { return _earnableItem; } }

        public void SetItem(EarnableItem item)
        {
            _earnableItem = item;
            ItemData itemData = ItemDataManager.Instance.itemDatas.Find(x => x.Item == _earnableItem.Item);
            if (itemData != null)
            {
                _itemImage.sprite = itemData.Sprite;
                _itemText.text = _earnableItem.Value > 0 ? ("x" + _earnableItem.Value) : string.Empty;
            }
            else
            {
                Debug.LogError("Error At Item Set");
            }
        }

        public void AddValue(int value)
        {
            _earnableItem.AddValue(value);
            _itemText.text = "x" + _earnableItem.Value;
        }

        private void OnValidate()
        {
            if (_itemText == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _itemText, transform);
            }
            if (_itemImage == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _itemImage, transform);
            }
        }

        /*********************************
         * If i had recieved data from backend 
         * i would use that so i can only
         * load necessary items via addressables
         * *******************************
        private AsyncOperationHandle _asyncItemOperation;
        public void SetIatem(int itemId, int value)
        {
            int itemValue = value;
            if (_asyncItemOperation.IsValid())
            {
                Addressables.Release(_asyncItemOperation);
            }
            _asyncItemOperation = Addressables.LoadAssetAsync<ItemData>(ItemConstants.SO_ITEM_DATA_ + itemId);
            _asyncItemOperation.Completed += ItemLoaded;

            void ItemLoaded(AsyncOperationHandle handle)
            {
                ItemData data = handle.Result as ItemData;
                if (data != null)
                {
                    _itemImage.sprite = data.Sprite;
                    _itemText.text = "x" + itemValue;
                }
                else
                {
                    Debug.LogError("Error At Item Load");
                }
            }
        }
        **********************************/
    }
}