using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsButton : MonoBehaviour
{
    [SerializeField] int Value;

    public void SetValue()
    {
        CrapsManager.Instance.Amount = Value;
        Debug.Log(CrapsManager.Instance.Amount);
    }
}
