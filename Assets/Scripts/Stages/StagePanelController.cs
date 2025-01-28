using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Util;
using WheelMechanic;

namespace StagePanel
{
    public class StagePanelController : MonoBehaviour
    {
        [SerializeField] private StageIndicatorBehaviour _baseStageIndicatorBehaviour;

        private List<StageIndicatorBehaviour> _stages = new();

        private int _currentStage = -1;

        private RectTransform _rectTransform;
        private float _stagePrefabSize;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _stagePrefabSize = _baseStageIndicatorBehaviour.GetComponent<RectTransform>().sizeDelta.x;
        }

        private void OnEnable()
        {
            WheelManager.OnGameStart += SetStage;
            WheelManager.OnNextWheel += NextStage;
            WheelManager.OnReset += ResetStage;
        }

        private void OnDisable()
        {
            WheelManager.OnGameStart -= SetStage;
            WheelManager.OnNextWheel -= NextStage;
            WheelManager.OnReset -= ResetStage;
        }

        private void ResetStage()
        {
            _rectTransform.localPosition += new Vector3(_stagePrefabSize * (_currentStage + 1), 0, 0);
            _currentStage = -1;
        }

        private void NextStage(SpecialStage stage)
        {
            _currentStage++;
            _stages[_currentStage].PassStage();
            _rectTransform.DOLocalMove(_rectTransform.transform.localPosition + new Vector3(-_stagePrefabSize, 0, 0), WheelConstants.WHEEL_SPIN_TIME / 4).OnComplete(() =>
            {
                _stages[_currentStage + 1].SetSprite(stage ? stage.StageIndexSprite : null);
            });
        }

        private void SetStage(List<SpecialStage> specialStages, int wheelCount)
        {
            for (int i = 0; i < wheelCount; i++)
            {
                SpecialStage stage = specialStages.Find(x => (i + 1) % x.EveryRaundOf == 0);
                if (i == 0) //We already have first stage item (We can't have less than 1 stage)
                {
                    if (_stages.Count != wheelCount)
                        _stages.Add(_baseStageIndicatorBehaviour);
                    _stages[i].SetSprite(stage ? stage.StageIndexSprite : null);
                }
                else
                {
                    if (_stages.Count != wheelCount)
                        _stages.Add(Instantiate(_baseStageIndicatorBehaviour, transform));
                    _stages[i].SetTextColor(stage ? stage.StageTextColor : Color.white);
                    _stages[i].SetTextValue(i + 1);
                }
            }
        }

        private void OnValidate()
        {
            ObjectFinder.FindObjectInChilderenWithType(ref _baseStageIndicatorBehaviour, transform);
        }
    }
}