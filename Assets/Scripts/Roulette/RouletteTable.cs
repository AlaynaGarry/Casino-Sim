using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RouletteTable : MonoBehaviour
{
    public static int playerChipBet;
    public bool hoverApplicable = true;
    [SerializeField] GameObject piece;
    public static string playerBetLocation = "";
    public static bool didPlayerWin = false;

    //creates a dictionary that holds the possible bets
    //the bool in the keyValuePair is a true false variable for oddes and evens false == oddes
    public Dictionary<int, KeyValuePair<string, bool>> Bets = new Dictionary<int, KeyValuePair<string, bool>>();
    
    public RouletteTable()
    {
        Bets.Add(1, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(2, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(3, new KeyValuePair<string, bool>("Red", false));

    }

    public void SetPlayerBet(int betValue)
    {
        playerChipBet = betValue;

        Debug.Log(playerChipBet);
    }

    public void setPlayerBetOnBoard(string bet)
    {
        playerBetLocation = bet;

        Debug.Log(playerBetLocation);
    }

    public bool checkPlayerWin(string playerBoardBet)
    {
       var gameWinningNumber = GameManager.Instance.GetRandomResult(1, 36, 1);
        if (playerBetLocation == "High" && gameWinningNumber[0] > 18)
        {
             return true;
        }
        else if(playerBetLocation == "Low" && playerBetLocation[0] < 19)
        {
            return true;
        }
        else if(Bets[gameWinningNumber[0]].Key.Equals("Black") && playerBetLocation == "Black")
        {
            return true;
        }
        else if (Bets[gameWinningNumber[0]].Key.Equals("Red") && playerBetLocation == "Red")
        {
            return true;
        }
        else if(Bets[gameWinningNumber[0]].Value == false && playerBetLocation == "Odd")
        {
            return true;
        }
        else if(Bets[gameWinningNumber[0]].Value == true && playerBetLocation == "Even")
        {
            return true;
        }
        else if(Bets[gameWinningNumber[0]].Equals(int.Parse(playerBoardBet)))
        {

        }
        return false;
    }

    public void resetGame()
    {

    }

    public void HoverOverPiece()
    {
        if (!hoverApplicable) return;
        var color = piece.GetComponent<Image>().tintColor;

        color = new Color(color.r, color.g, color.b, 0.5f);

        piece.GetComponent<Image>().tintColor = color;
        //Debug.Log("am here");
    }

    public void ExitHover()
    {
        if (!hoverApplicable) return;
        var color = piece.GetComponent<Image>().tintColor;

        color = new Color(color.r, color.g, color.b, 0);

        piece.GetComponent<Image>().tintColor = color;
        //Debug.Log("am gone");
    }
}
