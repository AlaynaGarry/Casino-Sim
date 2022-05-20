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
    protected int Amount;

    public abstract eResult? Resolve(int[] rolls);

    public void Win()
    {
        GameManager.Instance.gameData.intData["ChipsInHands"] += Amount * PayoutMultiplier + Amount;
        GameManager.Instance.gameData.intData["ChipsInLimbo"] -= Amount;
    }

    public void Lose()
    {
        GameManager.Instance.gameData.intData["ChipsInLimbo"] -= Amount;
    }
}
