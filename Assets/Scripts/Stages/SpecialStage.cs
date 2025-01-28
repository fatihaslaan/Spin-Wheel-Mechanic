using Item;
using System.Collections.Generic;
using UnityEngine;

namespace WheelMechanic
{
    [CreateAssetMenu(fileName = "Special Stage", menuName = "ScriptableObjects/Special Stage")]
    public class SpecialStage : ScriptableObject
    {
        [SerializeField] private string _stageName;
        [Min(2)][SerializeField] private int _everyRaundOf = 2; //Which raunds will become special stage
        [Min(1f)][SerializeField] private float _multiplier = 1f; //Multiply rarity chance only for current stage
        [SerializeField] private Sprite _wheelSprite;
        [SerializeField] private Sprite _wheelIndicatorSprite;
        [SerializeField] private Sprite _stageIndexSprite;
        [SerializeField] private Color _stageTextColor;

        [SerializeField] private List<Items> _includeItems; //Include this items in wheel every stage
        [SerializeField] private List<Items> _excludeItems; //Exclude this items from wheel every stage (like bomb)
        //Free Rewards That Will Gain On Stage Arrive In Order
        [SerializeField] private List<ItemList> _freeItemsAfterEveryStageByOrder;

        public string StageName { get { return _stageName; } }
        public int EveryRaundOf { get { return _everyRaundOf; } }
        public float Multiplier { get { return _multiplier; } }
        public Sprite WheelSprite { get { return _wheelSprite; } }
        public Sprite WheelIndicatorSprite { get { return _wheelIndicatorSprite; } }
        public Sprite StageIndexSprite { get { return _stageIndexSprite; } }
        public Color StageTextColor { get { return _stageTextColor; } }
        public List<Items> ExcludeItems { get { return _excludeItems; } }
        public List<Items> IncludeItems { get { return _includeItems; } }
        public List<ItemList> FreeItemsAfterEveryStageByOrder { get { return _freeItemsAfterEveryStageByOrder; } }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_stageName))
            {
                Debug.LogError("Stage Name Is Null");
            }
            if(_wheelSprite == null)
            {
                Debug.LogError("Wheel Sprite Is Null");
            }
            if (_wheelIndicatorSprite == null)
            {
                Debug.LogError("Wheel Indicator Sprite Is Null");
            }
            if (_stageIndexSprite == null)
            {
                Debug.LogError("Stage Index Sprite Is Null");
            }
        }
    }
}