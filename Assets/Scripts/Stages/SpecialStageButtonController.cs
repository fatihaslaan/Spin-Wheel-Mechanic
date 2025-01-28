using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WheelMechanic
{
    public class SpecialStageButtonController : MonoBehaviour
    {
        private List<SpecialStageButtonBehaviour> _specialStageButtonBehaviours = new();
        private AsyncOperationHandle _asyncItemOperation;

        private SpecialStageButtonBehaviour _specialStageButtonPrefab;
        private int _currentStageIndex = -1;

        private void OnDestroy()
        {
            ReleaseAsyncOperation();
        }

        private void ReleaseAsyncOperation()
        {
            if (_asyncItemOperation.IsValid())
            {
                Addressables.Release(_asyncItemOperation);
            }
        }

        private void OnEnable()
        {
            WheelManager.OnGameStart += SetSpecialStageButtons;
            WheelManager.OnReset += SetStages;
            WheelManager.OnNextWheel += OnNextStage;
        }

        private void OnDisable()
        {
            WheelManager.OnGameStart -= SetSpecialStageButtons;
            WheelManager.OnReset -= SetStages;
            WheelManager.OnNextWheel -= OnNextStage;
        }

        private void OnNextStage(SpecialStage stage)
        {
            _currentStageIndex++;
            List<int> temp = new();
            foreach (SpecialStageButtonBehaviour specialStageButton in _specialStageButtonBehaviours)
            {
                if(temp.Contains((((_currentStageIndex + 1) / specialStageButton.SpecialStage.EveryRaundOf) + 1) * specialStageButton.SpecialStage.EveryRaundOf))
                {
                    continue;
                }
                else
                {
                    temp.Add((((_currentStageIndex + 1) / specialStageButton.SpecialStage.EveryRaundOf) + 1) * specialStageButton.SpecialStage.EveryRaundOf);
                    specialStageButton.SetNextStage(_currentStageIndex + 1);
                }
            }
        }

        private void SetSpecialStageButtons(List<SpecialStage> specialStages, int wheelCount)
        {
            if (_specialStageButtonBehaviours.Count > 0 || specialStages.Count == 0) return;
            _asyncItemOperation = Addressables.LoadAssetAsync<GameObject>(WheelConstants.BUTTON_SPECIAL_STAGE);
            _asyncItemOperation.Completed += ItemLoaded;

            void ItemLoaded(AsyncOperationHandle handle)
            {
                _specialStageButtonPrefab = (handle.Result as GameObject).GetComponent<SpecialStageButtonBehaviour>();
                if (_specialStageButtonPrefab == null)
                {
                    Debug.LogError("Error At Special Stage Button Prefab Load");
                }
                else
                {
                    foreach (SpecialStage stage in specialStages)
                    {
                        SpecialStageButtonBehaviour stageButton = Instantiate(_specialStageButtonPrefab, transform);
                        stageButton.SetStage(stage, wheelCount);
                        _specialStageButtonBehaviours.Add(stageButton);
                    }
                    OnNextStage(null);
                }
            }
        }

        private void SetStages()
        {
            _currentStageIndex = -1;
            OnNextStage(null);
        }
    }
}
