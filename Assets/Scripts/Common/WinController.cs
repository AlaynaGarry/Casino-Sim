using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    [Serializable]
    public enum eCurrentGame
    {
        BLACKJACK,
        FIVECARD,
        CRAPS,
        ROULETTE
    }

    [SerializeField] Button continueButton;

    private void OnEnable()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Blackjack":
                continueButton.onClick.AddListener(() => ResetGame(0));
                break;
            case "5CardPoker":
                continueButton.onClick.AddListener(() => ResetGame(1));
                break;
            case "Craps":
                continueButton.onClick.AddListener(() => ResetGame(2));
                break;
            case "Roulette":
                continueButton.onClick.AddListener(() => ResetGame(3));
                break;
            default:
                break;
        }
    }

    public void ResetGame(int game)
    {
        eCurrentGame currentGame = (eCurrentGame) game;
        switch (currentGame)
        {
            case eCurrentGame.BLACKJACK:
                GameObject.FindObjectOfType<BlackjackManager>().RestartGame();
                break;
            case eCurrentGame.FIVECARD:
                GameObject.FindObjectOfType<PokerGameManager>().PlayAgain();
                break;
            case eCurrentGame.CRAPS:
                break;
            case eCurrentGame.ROULETTE:
                GameObject.FindObjectOfType<RouletteTable>().resetGame();
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }
}
