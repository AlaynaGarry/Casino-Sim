using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLineBet : CrapsBet
{
    public int? Point { get; private set; } = null;

    public MultiLineBet(int Amount)
    {
        Name = "Pass Bar";
        PayoutMultiplier = 1;
        this.Amount = Amount;
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
        int value = rolls[0] + rolls[1];

        if (Point == null)
        {
            if (value == 7 || value == 11) return eResult.WIN;
            if (value <= 3 || value == 12) return eResult.LOSE;

            Point = value;
        }
        else
        {
            if (value == 7) return eResult.LOSE;
            if (value == Point) return eResult.WIN;
        }

        return null;
    }
}
