using UnityEngine;

[CreateAssetMenu(fileName = "Special Stage", menuName = "ScriptableObjects/Special Stage")]
public class SpecialStage : ScriptableObject
{
    [SerializeField] private string _stageName;
    [SerializeField] private int _raund;
    [SerializeField] private float _multiplier;
    [SerializeField] private Sprite _wheelSprite;
    [SerializeField] private Sprite _wheelIndicatorSprite;
    [SerializeField] private Sprite _stageIndexSprite;

    public string StageName { get { return _stageName; } }
    public int Raund { get { return _raund; } }
    public float Multiplier { get { return _multiplier; } }
    public Sprite WheelSprite { get { return _wheelSprite; } }
    public Sprite WheelIndicatorSprite { get { return _wheelIndicatorSprite; } }
    public Sprite StageIndexSprite { get { return _stageIndexSprite; } }
}
