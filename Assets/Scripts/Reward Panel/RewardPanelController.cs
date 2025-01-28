using Item;
using System;
using UnityEngine;
using UnityEngine.UI;
using Util;
using WheelMechanic;

namespace RewardPanel
{
    public class RewardPanelController : MonoBehaviour
    {
        [SerializeField] private Button _leaveButton;
        [SerializeField] private ItemPanelController _itemPanelController;

        public static Action OnLeave;

        private void Awake()
        {
            _leaveButton.onClick.AddListener(LeaveWithRewards);
        }

        private void OnEnable()
        {
            WheelManager.OnRewardTaken += AddRewardToPanel;
            WheelManager.OnReset += ClearRewards;
            OnLeave += LeaveWithRewards;
        }

        private void OnDisable()
        {
            WheelManager.OnRewardTaken -= AddRewardToPanel;
            WheelManager.OnReset -= ClearRewards;
            OnLeave -= LeaveWithRewards;
        }

        private void AddRewardToPanel(EarnableItem item)
        {
            _itemPanelController.AddItemToPanel(item, WheelConstants.EO_REWARD_ITEM_DATA);
        }

        private void ClearRewards()
        {
            _itemPanelController.ClearPanel();
        }

        private void LeaveWithRewards()
        {
            PopupDisplayer.ShowItemPopup(WheelConstants.GAME_OVER_TITLE, WheelConstants.GAME_OVER_MESSAGE, _itemPanelController.GetItems(), WheelConstants.RESTART, WheelManager.OnItemsRecieved);
        }

        private void OnValidate()
        {
            if (_itemPanelController == null)
            {
                ObjectFinder.FindObjectInChilderenWithType<ItemPanelController>(ref _itemPanelController, transform);
            }
            if(_leaveButton == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _leaveButton, transform);
            }
        }
    }
}