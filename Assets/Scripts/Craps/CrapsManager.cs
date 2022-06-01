using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrapsManager : Singleton<CrapsManager>
{

    [SerializeField] GameObject displayUI;
    [SerializeField] TextMeshProUGUI displayUIMessage;

    [SerializeField] DiceDisplay Dice1;
    [SerializeField] DiceDisplay Dice2;

    [HideInInspector]
    public int Amount;

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
        string winOutput = "";
        string loseOutput = "";

        for(int i = 0; i < Bets.Count; i++)
        {
            var result = Bets[i].Resolve(rolls);

            if (result == CrapsBet.eResult.WIN)
            {
                if (!winOutput.Equals("")) winOutput += ", ";
                winOutput += Bets[i].Name;
                Bets.Remove(Bets[i--]);
            }
            
            if (result == CrapsBet.eResult.LOSE)
            {
                if (!loseOutput.Equals("")) loseOutput += ", ";
                loseOutput += Bets[i].Name;
                Bets.Remove(Bets[i--]);
            }

            if (result == CrapsBet.eResult.softWIN)
            {
                Bets.Remove(Bets[i--]);
            }
        }

        string output = "";

        if (!winOutput.Equals(""))
        {
            output = "You won the following bet(s): " + winOutput;
        }
        
        if (!loseOutput.Equals(""))
        {
            if (!output.Equals("")) output += "\n";
            output = "You lost the following bet(s): " + loseOutput;
        }

        if (output.Equals("")) output = "Nothing happened";

        Display(output);
    }

    public void Display(string messageToDisplay)
    {
        GameManager.Instance.state = GameManager.State.GAME_WIN;
        GameManager.Instance.pauser.paused = false;
        displayUI.SetActive(true);
        displayUIMessage.text = messageToDisplay;
    }
}