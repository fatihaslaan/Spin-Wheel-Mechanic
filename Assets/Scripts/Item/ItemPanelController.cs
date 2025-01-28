using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Item
{
    public class ItemPanelController : MonoBehaviour
    {
        private List<ItemUIBehaviour> _itemUIBehaviours = new();
        private AsyncOperationHandle _asyncItemOperation;

        private ItemUIBehaviour _itemUIBehaviourPrefab;

        private void OnDestroy()
        {
            ReleaseAsyncOperation();
        }

        public void ClearPanel()
        {
            for (int i = transform.childCount-1; i >=0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            _itemUIBehaviours.Clear();
            ReleaseAsyncOperation();
        }

        private void ReleaseAsyncOperation()
        {
            if (_asyncItemOperation.IsValid())
            {
                Addressables.Release(_asyncItemOperation);
            }
        }

        public List<EarnableItem> GetItems()
        {
            List<EarnableItem> earnableItems = new ();
            foreach (ItemUIBehaviour item in _itemUIBehaviours)
            {
                earnableItems.Add(item.EarnableItem);
            }
            return earnableItems;
        }

        public void AddItemToPanel(EarnableItem earnableItem, string itemUIName)
        {
            if(_itemUIBehaviourPrefab == null)
            {
                ReleaseAsyncOperation();
                _asyncItemOperation = Addressables.LoadAssetAsync<GameObject>(itemUIName);
                _asyncItemOperation.Completed += ItemLoaded;

                void ItemLoaded(AsyncOperationHandle handle)
                {
                    _itemUIBehaviourPrefab = (handle.Result as GameObject).GetComponent<ItemUIBehaviour>(); // Get Button
                    if (_itemUIBehaviourPrefab == null)
                    {
                        Debug.LogError("Error At Item UI Prefab Load");
                    }
                    else
                    {
                        AddItem();
                    }
                }
            }
            else
            {
                AddItem();
            }
            
            void AddItem()
            {
                ItemUIBehaviour itemUIBehaviour = _itemUIBehaviours.Find(x => x.EarnableItem.Item == earnableItem.Item); //Check If We Already Added
                if (itemUIBehaviour)
                {
                    itemUIBehaviour.AddValue(earnableItem.Value); //If So Only Add Value
                }
                else
                {
                    itemUIBehaviour = Instantiate(_itemUIBehaviourPrefab, transform); //If Not Create New With Id Order
                    itemUIBehaviour.SetItem(earnableItem);
                    _itemUIBehaviours.Add(itemUIBehaviour);
                    _itemUIBehaviours = _itemUIBehaviours.OrderBy(x => (int)x.EarnableItem.Item).ToList();

                    itemUIBehaviour.transform.SetSiblingIndex(_itemUIBehaviours.IndexOf(itemUIBehaviour));
                }
            }
        }
    }

}