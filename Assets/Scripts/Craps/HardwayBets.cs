using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwayBets : CrapsBet
{
    public int? WinRoll { get; private set; } = null;

    public HardwayBets(int Amount, string name, int multiplier, int winRoll)
    {
        Name = name;
        PayoutMultiplier = multiplier;
        this.Amount = Amount;
        this.WinRoll = winRoll;
    }

    public override eResult? Resolve(int[] rolls)
    {
        var gameDate = GameManager.Instance.gameData;
        var result = CheckRoll(rolls);

        if (result == eResult.WIN) Win();
        else if (result == eResult.LOSE) Lose();

        return result;
    }

    private eResult? CheckRoll(int[] rolls)
    {
        if (rolls[0] == WinRoll && rolls[1] == WinRoll)
        {
            return eResult.WIN;
        }

        return eResult.LOSE;
    }
}
