using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrapsBetButton : MonoBehaviour
{
    private enum eBetType
    {
        MultiBar,
        MultiLine,
        Hardway,
        OneWay,
        Point,
        Field
    }

    [Header("Bet Info")]
    [SerializeField] eBetType BetType;
    [SerializeField] string Name;
    [SerializeField] int Multiplier;
    [SerializeField] int WinRoll;

    public void PlaceBet()
    {
        try
        {
            Debug.Log(Name);
            var amount = CrapsManager.Instance.Amount;
            CrapsBet bet;

            switch (BetType)
            {
                case eBetType.MultiBar:
                    bet = new MultiBarBet(amount);
                    break;
                case eBetType.MultiLine:
                    bet = new MultiLineBet(amount);
                    break;
                case eBetType.Hardway:
                    bet = new HardwayBets(amount, Name, Multiplier, WinRoll);
                    break;
                case eBetType.OneWay:
                    bet = new OneWayBets(amount, Name, Multiplier, WinRoll);
                    break;
                case eBetType.Point:
                    bet = new PointBets(amount, Name);
                    break;
                case eBetType.Field:
                    bet = new Field(amount, Multiplier);
                    break;
                default:
                    throw new ArgumentNullException();
                    break;
            }
            GameManager.Instance.gameData.intData["ChipsInLimbo"] += amount;
            GameManager.Instance.gameData.intData["ChipsInHands"] -= amount;

            CrapsManager.Instance.AddBet(bet);
        }
        catch
        {

        }
    }

    public void StartRound()
    {
        CrapsManager.Instance.Round();
    }
}
