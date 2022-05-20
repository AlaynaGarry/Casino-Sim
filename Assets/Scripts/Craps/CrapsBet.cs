using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrapsBet
{
    public enum eResult
    {
        WIN,
        LOSE
    }

    public string Name;
    public int PayoutMultiplier;
    public int Amount;

    public abstract eResult Resolve(int[] rolls);

}
