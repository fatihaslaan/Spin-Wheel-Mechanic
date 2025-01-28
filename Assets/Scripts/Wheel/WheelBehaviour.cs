using Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace WheelMechanic
{
    public class WheelBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultWheelSprite;
        [SerializeField] private Sprite _defaultWheelIndicatorSprite;
        [SerializeField] private Image _wheelImage;
        [SerializeField] private Image _wheelIndicatorImage;

        private List<ItemUIBehaviour> _wheelItemBehaviours = new();

        public void LoadItemBehaviours()
        {
            _wheelItemBehaviours = _wheelImage.transform.GetComponentsInChildren<ItemUIBehaviour>().ToList();
        }

        public void SetWheelImage(Sprite wheelSprite, Sprite wheelIndicatorSprite)
        {
            _wheelImage.sprite = wheelSprite ? wheelSprite : _defaultWheelSprite;
            _wheelIndicatorImage.sprite = wheelIndicatorSprite ? wheelIndicatorSprite : _defaultWheelIndicatorSprite;
        }

        public void SetItems(WheelContent content)
        {
            if (_wheelItemBehaviours.Count != content.wheelItems.Length)
            {
                Debug.LogError("Content Size Is Wrong");
            }
            for (int i = 0; i < _wheelItemBehaviours.Count; i++)
            {
                _wheelItemBehaviours[i].SetItem(content.wheelItems[i]);
            }
        }

        public void SpinWheel(int wheelIndex, Action onSpinComplete)
        {
            //_wheelImage.DoRotate()
            onSpinComplete?.Invoke();
        }

        private void OnValidate()
        {
            if (_defaultWheelSprite == null)
            {
                Debug.LogError("Default Wheel Sprite Is Null");
            }
            if (_defaultWheelIndicatorSprite == null)
            {
                Debug.LogError("Default Wheel Sprite Is Null");
            }
            if (_wheelImage == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _wheelImage, transform, "Image_Wheel");
            }
            if (_wheelIndicatorImage == null)
            {
                ObjectFinder.FindObjectInChilderenWithName(ref _wheelIndicatorImage, transform, "Image_Wheel_Indicator");
            }
        }
    }
}