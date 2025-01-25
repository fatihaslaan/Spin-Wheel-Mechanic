using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemDataManager : Singleton<ItemDataManager>
{
    //We Wouldn't Need That Class If We Had Backend
    [HideInInspector] public List<ItemData> itemDatas = new();

    public Action OnDataLoad;

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
        _itemDataListAsyncOperation = Addressables.LoadAssetsAsync<ItemData>(Constants.SO_ITEM_LABEL_NAME, null);
        _itemDataListAsyncOperation.Completed += ItemsLoaded;
    }

    private void ItemsLoaded(AsyncOperationHandle<IList<ItemData>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            itemDatas = handle.Result.ToList();
            OnDataLoad?.Invoke();
        }
        else
        {
            Debug.LogError("Failed to load items.");
        }
    }
}