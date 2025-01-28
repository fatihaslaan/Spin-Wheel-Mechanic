using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Util;

namespace Core
{
    public abstract class BasePopupController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _messageText;

        [SerializeField] private Button _firstButton;
        [SerializeField] private Button _secondButton;
        [SerializeField] private TextMeshProUGUI _firstButtonText;
        [SerializeField] private TextMeshProUGUI _secondButtonText;

        public void SetTexts(string header, string message)
        {
            _headerText.text = header;
            _messageText.text = message;
        }

        public void ClosePopup(AsyncOperationHandle _handle) //Close Popup And Relase Asset From memory
        {
            Addressables.Release(_handle);
            Destroy(gameObject);
        }

        public void AddListenerToFirstButton(Action action)
        {
            _firstButton.onClick.AddListener(() => action?.Invoke());
        }

        public void AddListenerToSecondButton(Action action)
        {
            _secondButton.onClick.AddListener(() => action?.Invoke());
        }

        public void SetFirstButtonText(string text)
        {
            _firstButtonText.text = text;
        }

        public void SetSecondtButtonText(string text)
        {
            _secondButtonText.text = text;
        }

        public void CloseFirstButton()
        {
            _firstButton.gameObject.SetActive(false);
        }

        public void CloseSecondButton()
        {
            _secondButton.gameObject.SetActive(false);
        }

        protected abstract void OnOnValidate();

        private void OnValidate()
        {
            OnOnValidate();
            if (_headerText == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _headerText, transform, "Txt_Popup_Header");
            }
            if (_messageText == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _messageText, transform, "Txt_Popup_Message");
            }
            if (_firstButton == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _firstButton, transform, "Button_Popup_First_Button");
            }
            if (_secondButton == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _secondButton, transform, "Button_Popup_Second_Button");
            }
            if (_firstButton != null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _firstButtonText, _firstButton.transform);
            }
            if (_secondButton != null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _secondButtonText, _secondButton.transform);
            }
        }
    }
}