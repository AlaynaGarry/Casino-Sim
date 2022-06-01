using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBets : CrapsBet
{
    public int? Point { get; private set; } = null;

    public PointBets(int Amount, string name)
    {
        Name = name;
        PayoutMultiplier = 1;
        this.Amount = Amount;
    }

    public override eResult? Resolve(int[] rolls)
    {
        int value = rolls[0] + rolls[1];

        if(value != Point)
        {
            if(value != 2 && value != 3 && value != 7 && value != 11 && value != 12)
            {
                return eResult.WIN;
            }
        }

        return eResult.LOSE;
    }
}
