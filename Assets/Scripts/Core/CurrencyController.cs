using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMoney;

    private void Awake()
    {
        SetMoney(User.Money);
    }

    private void OnEnable()
    {
        User.OnMoneyChanged += SetMoney;
    }

    private void OnDisable()
    {
        User.OnMoneyChanged -= SetMoney;
    }

    private void SetMoney(int newMoney)
    {
        _textMoney.text = newMoney.ToString();
    }

    private void OnValidate()
    {
        if(_textMoney == null)
        {
            _textMoney = GetComponent<TextMeshProUGUI>();
            if( _textMoney == null )
            {
                Debug.LogError("Failed To Set Currency Text");
            }
        }
    }
}