using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrapsManager : Singleton<CrapsManager>
{
    List<CrapsBet> Bets = new List<CrapsBet>();

    public void Round()
    {
        if (Bets.Count == 0) return;

        var rolls = GameManager.Instance.GetRandomResult(1, 6, 2);

        ResolveBets(rolls);
    }

    public void AddBet(CrapsBet bet)
    {
        Bets.Add(bet);
    }

    public void RemoveBet(CrapsBet bet)
    {
        Bets.Remove(bet);
    }

    public void ResolveBets(int[] rolls)
    {
        string output = "";

        foreach(CrapsBet bet in Bets)
        {
            var result = bet.Resolve(rolls);

            if (result == CrapsBet.eResult.WIN)
            {
                if (!output.Equals("")) output += ", ";
                output += bet.Name;
                Bets.Remove(bet);
            }
        }

        if (!output.Equals(""))
        {
            GameManager.Instance.OnWin("You won the following bet(s): " + output);
        }
        else
        {

        }
    }
}