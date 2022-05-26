using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrapsManager : Singleton<CrapsManager>
{

    [SerializeField] GameObject displayUI;
    [SerializeField] TextMeshProUGUI displayUIMessage;

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

        foreach(CrapsBet bet in Bets)
        {
            var result = bet.Resolve(rolls);

            if (result == CrapsBet.eResult.WIN)
            {
                if (!winOutput.Equals("")) winOutput += ", ";
                winOutput += bet.Name;
                Bets.Remove(bet);
            }
            
            if (result == CrapsBet.eResult.WIN)
            {
                if (!loseOutput.Equals("")) loseOutput += ", ";
                loseOutput += bet.Name;
                Bets.Remove(bet);
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

        Display(output);
    }

    public void Display(string messageToDisplay)
    {
        GameManager.Instance.state = GameManager.State.GAME_WIN;
        GameManager.Instance.pauser.paused = false;
        if (GameManager.Instance.winMusic) AudioManager.Instance.PlayMusic(GameManager.Instance.winMusic);
        displayUI.SetActive(true);
        displayUIMessage.text = messageToDisplay;
    }
}