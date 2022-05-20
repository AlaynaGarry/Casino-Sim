using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBarBet : CrapsBet
{
    public int Point { get; private set; }

    public MultiBarBet(int Point)
    {
        this.Point = Point;
        Name = "Don't Pass Bar";
        PayoutMultiplier = 1;
    }

    public override eResult Resolve(int[] rolls)
    {
        
        return eResult.LOSE;
    }
}
