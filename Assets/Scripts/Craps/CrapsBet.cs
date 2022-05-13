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

    public string BetName;
    public int PayoutMultiplier;

    public abstract eResult Resolve();
}
