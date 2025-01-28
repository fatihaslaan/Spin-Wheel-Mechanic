using Item;
using RewardPanel;
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace WheelMechanic
{
    public class WheelManager : MonoBehaviour, IInventoryManager
    {
        public static Action<EarnableItem> OnRewardTaken;
        public static Action<EarnableItem> OnSpinComplete;
        public static Action<SpecialStage> OnWheelSet;
        public static Action<SpecialStage> OnNextWheel;

        public static Action OnItemsRecieved;

        public static Action<List<SpecialStage>, int> OnGameStart;
        public static Action OnReset;

        private int _currentWheelIndex;
        private EarnableItem _itemFromSpin;

        private List<SpecialStage> _specialStages;
        private int _wheelCount;

        private void StartGame(List<SpecialStage> specialStages, int wheelCount)
        {
            _specialStages = specialStages;
            _wheelCount = wheelCount;
            OnGameStart?.Invoke(_specialStages, _wheelCount);
            SetCurrentWheel();
        }

        private void OnEnable()
        {
            WheelDataCreator.SetGameData += StartGame;
            OnSpinComplete += SpinCompleted;
            OnItemsRecieved += Restart;
        }

        private void OnDisable()
        {
            WheelDataCreator.SetGameData -= StartGame;
            OnSpinComplete -= SpinCompleted;
            OnItemsRecieved -= Restart;
        }

        private void SetCurrentWheel()
        {
            _currentWheelIndex++;
            SpecialStage stage = _specialStages.Find(x => _currentWheelIndex % x.EveryRaundOf == 0);
            OnWheelSet?.Invoke(stage);
            CheckFreeRewards(stage);
        }

        private void CheckFreeRewards(SpecialStage stage)
        {
            if (stage != null) //Check Free Rewards
            {
                if (stage.FreeItemsAfterEveryStageByOrder.Count >= (_currentWheelIndex / stage.EveryRaundOf))
                {
                    if (stage.FreeItemsAfterEveryStageByOrder[(_currentWheelIndex / stage.EveryRaundOf) - 1].Items.Count > 0)
                    {
                        List<EarnableItem> items = new List<EarnableItem>(stage.FreeItemsAfterEveryStageByOrder[(_currentWheelIndex / stage.EveryRaundOf) - 1].Items);
                        foreach (EarnableItem item in items)
                            OnRewardTaken?.Invoke(item);
                        PopupDisplayer.ShowItemPopup(WheelConstants.FREE_ITEM_FROM_STAGE_TITLE, WheelConstants.FREE_ITEM_EARNED_FROM_STAGE_MESSAGE, items);
                    }
                }
            }
        }

        private void SpinCompleted(EarnableItem wheelItem)
        {
            _itemFromSpin = wheelItem;
            ItemDataManager.Instance.itemDatas.Find(x => x.Item == wheelItem.Item).AddToInventory(this);
        }

        public void ClearInventory()
        {
            PopupDisplayer.ShowItemPopup(WheelConstants.BOMB_EXPLODE_TITLE, WheelConstants.BOMB_EXPLODE_MESSAGE, new List<EarnableItem>() { _itemFromSpin }, WheelConstants.RESTART, Restart, WheelConstants.CONTINUE, Continue);
        }

        public void ItemAddedToInventory()
        {
            OnRewardTaken?.Invoke(_itemFromSpin);
            if (_currentWheelIndex != _wheelCount)
            {
                SetCurrentWheel();
                OnNextWheel?.Invoke(_specialStages.Find(x => _currentWheelIndex % x.EveryRaundOf == 0));
            }
            else
                RewardPanelController.OnLeave?.Invoke();
        }

        private void Restart()
        {
            _currentWheelIndex = 0;
            OnReset?.Invoke();
            StartGame(_specialStages, _wheelCount);
        }

        private void Continue()
        {
            User.UseMoney(WheelConstants.CONTINUE_COST, null, () =>
            {
                PopupDisplayer.ShowItemPopup(WheelConstants.NO_MONEY_TITLE, WheelConstants.NO_MONEY_MESSAGE, null, WheelConstants.RESTART, Restart);
            });
        }
    }
}