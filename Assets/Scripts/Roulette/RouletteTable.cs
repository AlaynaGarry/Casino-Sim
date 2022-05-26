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
        Bets.Add(4, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(5, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(6, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(7, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(8, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(9, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(10, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(11, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(12, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(13, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(14, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(15, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(16, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(17, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(18, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(19, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(20, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(21, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(22, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(23, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(24, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(25, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(26, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(27, new KeyValuePair<string, bool>("Red", false));
        Bets.Add(28, new KeyValuePair<string, bool>("Black", true));
        Bets.Add(29, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(30, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(31, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(32, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(33, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(34, new KeyValuePair<string, bool>("Red", true));
        Bets.Add(35, new KeyValuePair<string, bool>("Black", false));
        Bets.Add(36, new KeyValuePair<string, bool>("Red", true));
    }

    public void resetGame()
    {

    }

    public void SetPlayerBet(int betValue)
    {
        Debug.Log(playerChipBet);
        playerChipBet = betValue;


    }

    public void setPlayerBetOnBoard(string bet)
    {
        playerBetLocation = bet;
        checkPlayerWin(playerBetLocation);

    }

    public bool checkPlayerWin(string playerBoardBet)
    {
        var gameWinningNumber = GameManager.Instance.GetRandomResult(1, 36, 1);
        int tempNum;
        int.TryParse(playerBoardBet, out tempNum);
        if (playerBetLocation.Equals("High") && gameWinningNumber[0] > 18)
        {
             return true;
        }
        else if(playerBetLocation.Equals("Low") && gameWinningNumber[0] < 19)
        {
            return true;
        }
        else if (Bets[gameWinningNumber[0]].Key.Equals("Black") && playerBetLocation == "Black")
        {
            return true;
        }
        else if (Bets[gameWinningNumber[0]].Key.Equals("Red") && playerBetLocation == "Red")
        {
            return true;
        }
        else if (Bets[gameWinningNumber[0]].Value == false && playerBetLocation.Equals("Odd"))
        {
            return true;
        }
        else if (Bets[gameWinningNumber[0]].Value == true && playerBetLocation.Equals("Even"))
        {
            return true;
        }
        else if(Bets[gameWinningNumber[0]].Equals(tempNum))
        {
            return true;
        }
        return false;
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
