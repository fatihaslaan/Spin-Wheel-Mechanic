using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelBehaviour : MonoBehaviour
{
    private List<WheelItemBehaviour> _wheelItemBehaviours = new();

    private void Awake()
    {
        LoadItemBehaviours();
    }

    private void LoadItemBehaviours()
    {
        _wheelItemBehaviours = transform.GetComponentsInChildren<WheelItemBehaviour>().ToList();
        Debug.Log("Wheel Reward Count = " + _wheelItemBehaviours.Count);
    }

    public void SetItems()
    {

    }
}
