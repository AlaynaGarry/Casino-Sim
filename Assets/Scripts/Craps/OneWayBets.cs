using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OneWayBets : CrapsBet
{
    public int? WinRoll { get; private set; } = null;

    public OneWayBets(int Amount, string name, int multiplier, int winRoll)
    {
        Name = name;
        PayoutMultiplier = multiplier;
        this.Amount = Amount;
        this.WinRoll = winRoll;
    }

    public override eResult? Resolve(int[] rolls)
    {
        int value = rolls[0] + rolls[1];

        if (value == WinRoll)
        {
            return eResult.WIN;
        }

        return eResult.LOSE;
    }
}

