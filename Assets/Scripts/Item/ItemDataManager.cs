using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Item
{
    public class ItemDataManager : Singleton<ItemDataManager>
    {
        //We Wouldn't Need That Class If We Had Backend
        //For Loading Items Once
        public static Action OnDataLoad;
        public List<ItemData> itemDatas { get { return _itemDatas; } }
        private List<ItemData> _itemDatas = new();

        private AsyncOperationHandle<IList<ItemData>> _itemDataListAsyncOperation;

        protected override void Awake()
        {
            base.Awake();
            LoadItems();
        }

        private void OnDestroy()
        {
            Addressables.Release(_itemDataListAsyncOperation);
        }

        private void LoadItems()
        {
            _itemDataListAsyncOperation = Addressables.LoadAssetsAsync<ItemData>(ItemConstants.ITEM, null);
            _itemDataListAsyncOperation.Completed += ItemsLoaded;
        }

        private void ItemsLoaded(AsyncOperationHandle<IList<ItemData>> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _itemDatas = handle.Result.ToList();
                OnDataLoad?.Invoke();
            }
            else
            {
                Debug.LogError("Failed to load items.");
            }
        }
    }
}