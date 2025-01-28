using TMPro;
using UnityEngine;
using User;

namespace Currency
{
    public class CurrencyController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textMoney;

        private void Awake()
        {
            SetMoney(UserCurrency.Money);
        }

        private void OnEnable()
        {
            UserCurrency.OnMoneyChanged += SetMoney;
        }

        private void OnDisable()
        {
            UserCurrency.OnMoneyChanged -= SetMoney;
        }

        private void SetMoney(int newMoney)
        {
            _textMoney.text = newMoney.ToString();
        }

        private void OnValidate()
        {
            if (_textMoney == null)
            {
                _textMoney = GetComponent<TextMeshProUGUI>();
                if (_textMoney == null)
                {
                    Debug.LogError("Failed To Set Currency Text");
                }
            }
        }
    }
}