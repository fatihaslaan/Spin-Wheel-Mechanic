using System;

public class User
{
    private static int _money = 150;

    public static int Money { get { return _money; } }

    public static Action<int> OnMoneyChanged;


    public static void UseMoney(int money, Action onSuccess, Action onFail)
    {
        if (_money >= money)
        {
            _money -= money;
            onSuccess?.Invoke();
            OnMoneyChanged?.Invoke(_money);
        }
        else
        {
            onFail?.Invoke();
        }
    }

    public static void AddMoney(int money)
    {
        _money += money;
        OnMoneyChanged?.Invoke(_money);
    }
}