using Item;
using UnityEngine;

public class WaitAddressableDownload : MonoBehaviour
{
    private void Awake()
    {
        ItemDataManager.OnDataLoad += () =>
        {
            Destroy(gameObject);
        };
    }
}
