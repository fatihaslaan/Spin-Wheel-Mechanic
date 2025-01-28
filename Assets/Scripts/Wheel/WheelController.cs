using UnityEngine;
using UnityEngine.UI;
using Util;

namespace WheelMechanic
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private WheelBehaviour _wheelBehaviour;
        [SerializeField] private Button _wheelSpinButton;

        private WheelContent wheelContent = new();

        private void Awake()
        {
            InitWheel();
        }

        private void InitWheel()
        {
            _wheelBehaviour.LoadItemBehaviours();
            _wheelSpinButton.onClick.AddListener(SpinWheel);
        }

        private void OnEnable()
        {
            WheelDataCreator.OnWheelContentCreated += SetWheelData;
            WheelManager.OnWheelSet += ChangeWheelSprite;
        }

        private void OnDisable()
        {
            WheelDataCreator.OnWheelContentCreated -= SetWheelData;
            WheelManager.OnWheelSet -= ChangeWheelSprite;
        }

        public void SetWheelData(WheelContent content)
        {
            wheelContent = content;
            _wheelBehaviour.SetItems(wheelContent);
        }

        public void ChangeWheelSprite(SpecialStage stage) //Change Sprite Of wheel according to current stage
        {
            _wheelBehaviour.SetWheelImage(stage ? stage.WheelSprite : null, stage ? stage.WheelIndicatorSprite : null);
        }

        public void SpinWheel()
        {
            int randomSliceIndex = GetRandomWheelSlice();
            _wheelBehaviour.SpinWheel(GetRandomWheelSlice(), OnSpinComplete);

            void OnSpinComplete()
            {
                //I think getting item from backend would be safer approach
                WheelManager.OnSpinComplete?.Invoke(wheelContent.wheelItems[randomSliceIndex]);
            }

            int GetRandomWheelSlice()
            {
                return UnityEngine.Random.Range(0, WheelConstants.WHEEL_SLICE_COUNT);
            }
        }

        private void OnValidate()
        {
            if (_wheelSpinButton == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _wheelSpinButton, transform);
            }
            if (_wheelBehaviour == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _wheelBehaviour, transform);
            }
        }
    }
}