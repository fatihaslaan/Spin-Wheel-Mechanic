using Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace WheelMechanic
{
    public class WheelDataCreator : MonoBehaviour
    {
        [SerializeField] private List<Items> _includeInEveryStage = new List<Items> { Items.Bomb };
        [SerializeField] private List<SpecialStage> _specialStages;
        private Dictionary<ItemRarity, float> tempRarityChances = new();
        [Min(1)] [SerializeField] private int _wheelCount = 1;
        private List<Items> _tempIncludeList;

        private float _rarityMultiplier = 1f;

        [SerializeField] private bool _useEditorList;
        [SerializeField] private List<WheelContent> _wheelDatas = new(); //We Could Have Set That Via Backend
        public static Action<List<SpecialStage>, int> SetGameData;
        private int _currentIndex;

        public static Action<WheelContent> OnWheelContentCreated;

        private Random _random = new();

        private void OnEnable()
        {
            ItemDataManager.OnDataLoad += ItemDataLoaded;
            WheelManager.OnWheelSet += CreateWheelContent;
            WheelManager.OnReset += InitRarityValues;
        }

        private void OnDisable()
        {
            ItemDataManager.OnDataLoad -= ItemDataLoaded;
            WheelManager.OnWheelSet -= CreateWheelContent;
            WheelManager.OnReset -= InitRarityValues;
        }

        private void ItemDataLoaded()
        {
            SetGameData?.Invoke(_specialStages, _wheelCount);
        }

        private void Awake()
        {
            InitRarityValues();
        }

        private void InitRarityValues()
        {
            _currentIndex = 0;
            if (!_useEditorList)
            {
                _wheelDatas.Clear();
                tempRarityChances.Clear();
                foreach (var item in ItemConstants.RARITY_CHANCES)
                {
                    tempRarityChances.Add(item.Key, item.Value);
                }
            }
        }

        private void CreateWheelContent(SpecialStage stage)
        {
            WheelContent wheelContent = new();
            if(_useEditorList)
            {
                wheelContent = _wheelDatas[_currentIndex];
                _currentIndex++;
            }
            else
            {
                _tempIncludeList = new List<Items>(_includeInEveryStage);
                SetStageProperties(stage);
                wheelContent = GenerateWheelContent();
                tempRarityChances = tempRarityChances.Where(x => !WheelConstants.EXCLUDED_FROM_MULTIPLIER.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value * WheelConstants.CHANCE_MULTIPLIER_BY_EACH_STAGE);

            }
            OnWheelContentCreated?.Invoke(wheelContent);

            void SetStageProperties(SpecialStage stage)
            {
                if (stage != null)
                {
                    foreach (Items includeItem in stage.IncludeItems)
                    {
                        if (_tempIncludeList.Count == WheelConstants.WHEEL_SLICE_COUNT)
                        {
                            _tempIncludeList.RemoveAt(0);
                        }
                        _tempIncludeList.Add(includeItem);
                    }
                    _tempIncludeList.RemoveAll(x => stage.ExcludeItems.Contains(x));

                    _rarityMultiplier = stage.Multiplier;
                }
                else
                {
                    _rarityMultiplier = 1f;
                }
            }
        }

        public WheelContent GenerateWheelContent()
        {
            WheelContent wheelContent = new WheelContent();

            HashSet<int> assignedItemIndexes = new HashSet<int>();
            AddIncludedList();

            for (int i = 0; i < WheelConstants.WHEEL_SLICE_COUNT; i++)
            {
                ItemData tempData;
                if(assignedItemIndexes.Contains(i))
                {
                    tempData = ItemDataManager.Instance.itemDatas.Find(x => x.Item == _tempIncludeList[0]); //Asign Included Items To Wheel
                    _tempIncludeList.RemoveAt(0);
                }
                else
                {
                    tempData = GetRandomItem(GetRarityValue() * _rarityMultiplier);
                }
                wheelContent.wheelItems[i] = new EarnableItem(tempData.Item, _random.Next(tempData.MinDropValue, tempData.MaxDropValue + 1));
            }
            return wheelContent;

            float GetRarityValue()
            {
                return (float)(_random.NextDouble() * tempRarityChances.Values.Sum());
            }

            ItemData GetRandomItem(float rarityVal) //Get Random Item With Rarity (If Not Using Editor Data)
            {
                float tempVal = 0f;
                ItemRarity selectedRarity = ItemRarity.Legendary;
                foreach (ItemRarity rarity in ItemConstants.RARITY_CHANCES.Keys)
                {
                    tempVal += ItemConstants.RARITY_CHANCES[rarity];
                    if (rarityVal <= tempVal)
                    {
                        selectedRarity = rarity;
                        break;
                    }
                }
                List<ItemData> tempDatas = ItemDataManager.Instance.itemDatas.FindAll(x => (x.Rarity == selectedRarity) && !x.DontIncludeInWheel);
                return tempDatas[_random.Next(0, tempDatas.Count)];
            }

            void AddIncludedList() //Add Included Items To Wheel Like Bomb (If Not Using Editor Data)
            {
                foreach (Items item in _tempIncludeList)
                {
                    int index;
                    do
                    {
                        index = _random.Next(0, WheelConstants.WHEEL_SLICE_COUNT);
                    } while (assignedItemIndexes.Contains(index));

                    assignedItemIndexes.Add(index);
                }
            }
        }

        private void OnValidate()
        {
            if (_useEditorList)
            {
                if (_wheelDatas.Count < 1)
                {
                    Debug.LogError("Can't Be Less Than 1 Wheel Count");
                    _wheelDatas.Add(new WheelContent());
                }
                if (_wheelDatas.Count > _wheelCount)
                {
                    while (_wheelDatas.Count > _wheelCount)
                    {
                        _wheelDatas.RemoveAt(0);
                    }
                }
                if(_wheelDatas.Count < _wheelCount)
                {
                    while (_wheelDatas.Count < _wheelCount)
                    {
                        _wheelDatas.Add(new WheelContent());
                    }
                }
                for (int i = 0; i < _wheelDatas.Count; i++)
                {
                    if (_wheelDatas[i].wheelItems.Length != WheelConstants.WHEEL_SLICE_COUNT)
                    {
                        Debug.LogError("Don't change the Size of Wheel Slice Count");
                        Array.Resize(ref _wheelDatas[i].wheelItems, WheelConstants.WHEEL_SLICE_COUNT);
                    }
                    for(int j = 0; j< _wheelDatas[i].wheelItems.Length;j++)
                    {
                        if (_wheelDatas[i].wheelItems[j].Item == Items.None)
                        {
                            Debug.LogError("Can't Be Null Changing To Cash");
                            _wheelDatas[i].wheelItems[j] = new EarnableItem(Items.Cash, 100);
                        }
                        if(_wheelDatas[i].wheelItems[j].Item != Items.Bomb && _wheelDatas[i].wheelItems[j].Value < 1)
                        {
                            Debug.LogError("Can't Be 0 Value Unless It's Bomb");
                        }
                    }
                }

            }
            else
            {
                _specialStages = _specialStages.OrderByDescending(x => x.EveryRaundOf).ToList();
                if (_tempIncludeList.Count > WheelConstants.WHEEL_SLICE_COUNT)
                {
                    Debug.LogError("Include List Can't Be Higher Than Slice Count");
                    _tempIncludeList.Clear();
                }
            }
        }
    }
}