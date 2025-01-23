using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelItemBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemText;
    private Image _itemImage;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
    }

    public void SetItem(uint rewardId, uint count)
    {

    }
}