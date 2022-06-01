using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : CrapsBet
{
    public int? Point { get; private set; } = null;

    public Field(int Amount, int multiplier)
    {
        Name = "Field";
        PayoutMultiplier = multiplier;
        this.Amount = Amount;
    }

    public override eResult? Resolve(int[] rolls)
    {
        int value = rolls[0] + rolls[1];

        if (value != Point)
        {
            if(value == 2 || value == 12)
            {
                return eResult.softWIN;
            }

            if ( value == 3 || value == 4 || value == 9 || value == 10 || value == 11)
            {
                return eResult.WIN;
            }
        }

        return eResult.LOSE;
    }
}
