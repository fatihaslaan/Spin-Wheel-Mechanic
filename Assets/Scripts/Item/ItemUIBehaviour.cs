using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Util;
using WheelMechanic;

namespace Item
{
    public class ItemUIBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemText;
        [SerializeField] private Image _itemImage;


        private EarnableItem _earnableItem;
        public EarnableItem EarnableItem { get { return _earnableItem; } }

        private AsyncOperationHandle _asyncItemOperation;
        private GameObject _rarityBackground;

        private void OnDestroy()
        {
            ReleaseAsyncOperation();
        }

        private void ReleaseAsyncOperation()
        {
            if (_asyncItemOperation.IsValid())
            {
                Addressables.Release(_asyncItemOperation);
            }
        }

        public void SetItem(EarnableItem item)
        {
            _earnableItem = item;
            ItemData itemData = ItemDataManager.Instance.itemDatas.Find(x => x.Item == _earnableItem.Item);
            if (itemData != null)
            {
                _itemImage.sprite = itemData.Sprite;
                _itemText.text = _earnableItem.Value > 0 ? ("x" + _earnableItem.Value) : string.Empty;

                if (_rarityBackground)
                {
                    Destroy(_rarityBackground);
                }
                ReleaseAsyncOperation();
                if (CheckAddressableKeyExists(ItemConstants.IMAGE_ITEM_BACKGROUND_ + itemData.Rarity))
                {
                    _asyncItemOperation = Addressables.LoadAssetAsync<GameObject>(ItemConstants.IMAGE_ITEM_BACKGROUND_ + itemData.Rarity);
                    _asyncItemOperation.Completed += ItemLoaded;

                    void ItemLoaded(AsyncOperationHandle handle)
                    {
                        GameObject rarityBackground = handle.Result as GameObject; // Get Button
                        if (rarityBackground != null)
                        {
                            _rarityBackground = Instantiate(rarityBackground, transform);
                            _rarityBackground.transform.SetSiblingIndex(0);
                            _rarityBackground.transform.position = _itemImage.transform.position;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Error At Item Set");
            }
        }

        private bool CheckAddressableKeyExists(string key)
        {
            foreach (var locator in Addressables.ResourceLocators)
            {
                if (locator.Keys.Contains(key))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddValue(int value)
        {
            float temp = (float)_earnableItem.Value;

            DOTween.To(() => temp, x =>
            {
                temp = x;
                _itemText.text = "x" + Mathf.FloorToInt(temp);
            }, _earnableItem.Value + value, WheelConstants.WHEEL_SPIN_TIME / 4).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _earnableItem.AddValue(value);
            });

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