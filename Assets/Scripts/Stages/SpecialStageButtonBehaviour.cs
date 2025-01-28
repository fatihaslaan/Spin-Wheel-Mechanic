using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace WheelMechanic
{
    public class SpecialStageButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Image _freeItemImage;
        [SerializeField] private TextMeshProUGUI _stageNameText;
        [SerializeField] private TextMeshProUGUI _stageValueText;

        private SpecialStage _specialStage;
        public SpecialStage SpecialStage { get { return _specialStage; } }
        private int _specialStageIndex;
        private int _wheelCount;

        public void SetNextStage(int nextStage)
        {
            if (_specialStageIndex != (nextStage / _specialStage.EveryRaundOf) + 1) //Did We Already Set It?
            {
                _specialStageIndex = (nextStage / _specialStage.EveryRaundOf) + 1;

                _button.enabled = false;
                _freeItemImage.enabled = false;
                if (_wheelCount < _specialStage.EveryRaundOf * _specialStageIndex) //Is Next Stage Available?
                {
                    gameObject.SetActive(false);
                    return;
                }
                else if(!gameObject.activeSelf) gameObject.SetActive(true);

                _stageValueText.text = (_specialStage.EveryRaundOf * _specialStageIndex).ToString();
                if (_specialStage.FreeItemsAfterEveryStageByOrder.Count > _specialStageIndex - 1) //Is There Free Items For Us?
                {
                    if (_specialStage.FreeItemsAfterEveryStageByOrder[_specialStageIndex - 1].Items.Count > 0)
                    {
                        _freeItemImage.enabled = true;
                        _freeItemImage.sprite = ItemDataManager.Instance.itemDatas.Find(x => x.Item == _specialStage.FreeItemsAfterEveryStageByOrder[_specialStageIndex - 1].Items[0].Item).Sprite; //Show First Free Reward Of Stage
                        _button.enabled = true;
                        _button.onClick.RemoveAllListeners();
                        _button.onClick.AddListener(() =>
                        {
                            PopupDisplayer.ShowItemPopup(WheelConstants.FREE_ITEM_FROM_STAGE_TITLE, WheelConstants.FREE_ITEM_FROM_STAGE_MESSAGE, _specialStage.FreeItemsAfterEveryStageByOrder[_specialStageIndex - 1].Items);
                        });
                    }
                }
            }
        }

        public void SetStage(SpecialStage stage, int wheelCount)
        {
            _specialStage = stage;
            _image.sprite = stage.StageIndexSprite;
            _stageNameText.text = stage.StageName;
            _wheelCount = wheelCount;
        }

        private void OnValidate()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
                if (_image == null)
                {
                    Debug.LogError("Failed To Set Image At Stage Button Behaviour");
                }
            }
            if (_button == null)
            {
                _button = GetComponent<Button>();
                if (_button == null)
                {
                    Debug.LogError("Failed To Set Button At Stage Button Behaviour");
                }
            }
            if (_freeItemImage == null)
                ObjectFinder.FindObjectInChilderenWithName(ref _freeItemImage, transform, "Image_Special_Stage_Free_Item");
            if (_stageNameText == null)
                ObjectFinder.FindObjectInChilderenWithName(ref _stageNameText, transform, "Txt_Special_Stage_Name");
            if (_stageValueText == null)
                ObjectFinder.FindObjectInChilderenWithName(ref _stageValueText, transform, "Txt_Speacil_Stage_Index_Value");
        }
    }
}
