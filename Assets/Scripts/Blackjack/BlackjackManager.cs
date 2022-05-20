using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackjackManager : MonoBehaviour
{
    [SerializeField] GameData blackjackData;
    [SerializeField] TextMeshProUGUI currentBetText;
    [SerializeField] AudioClip failToSucceedSound;
    [SerializeField] AudioClip succeedSound;
    [SerializeField] List<GameObject> playerHand = new List<GameObject>();
    [SerializeField] List<GameObject> dealerHand = new List<GameObject>();
    [SerializeField] List<GameObject> actionButtons = new List<GameObject>();
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject clearBetButton;
    [SerializeField] GameObject betContainer;


    private CardDeck blackjackDeck;

    private int lastPlayerHandIndex = 0;
    private int lastDealerHandIndex = 0;

    private bool isRunning;

    private void Start()
    {
        ClearBet();
        blackjackDeck = GetComponent<CardDeck>();
        actionButtons.ForEach(button => button.SetActive(false));
    }



    public void StartGame()
    {
        if (GameManager.Instance.gameData.intData["ChipsInLimbo"] > 0)
        {
            if (succeedSound) AudioManager.Instance.PlaySFX(succeedSound);
            startButton.SetActive(false);
            betContainer.SetActive(false);
            actionButtons.ForEach(button => button.SetActive(true));
            blackjackDeck.CreateDeck();
            DealerFirstTurn();
            PlayerStart();
        }
        else
        {
            if (failToSucceedSound) AudioManager.Instance.PlaySFX(failToSucceedSound);
        }
    }

    public void RestartGame()
    {
        if (succeedSound) AudioManager.Instance.PlaySFX(succeedSound);
        startButton.SetActive(true);
        betContainer.SetActive(true);
        actionButtons.ForEach(button => button.SetActive(false));
        playerHand.ForEach(card => card.SetActive(false));
        dealerHand.ForEach(card => card.SetActive(false));
    }

    private void DealerFirstTurn()
    {
        var card = blackjackDeck.DealHand(1)[0];
        dealerHand[lastDealerHandIndex].AddComponent<Card>();
        dealerHand[lastDealerHandIndex].GetComponent<Card>().Suit = card.Suit;
        dealerHand[lastDealerHandIndex].GetComponent<Card>().Value = card.Value;
        dealerHand[lastDealerHandIndex].GetComponent<Card>().cardFaceDown = true;
        dealerHand[lastDealerHandIndex].SetActive(true);
        lastDealerHandIndex++;
        //if busted, lose
        List<Card> cardsInHand = new List<Card>();

        foreach (var cardObject in dealerHand)
        {
            if (cardObject.TryGetComponent(out Card cardInHand)) cardsInHand.Add(cardInHand);
        }
        int[] currentDealerHandValues = CardDeck.valueOfHand(cardsInHand);
        blackjackData.intData["DealerHandValue"] = currentDealerHandValues[0];
        blackjackData.intData["DealerHandValueHighAce"] = currentDealerHandValues[1];
        RunDealerTurn();
    }

    public void RunDealerTurn()
    {
        if (blackjackData.intData["DealerHandValue"] > 17) return;
        var card = blackjackDeck.DealHand(1)[0];
        dealerHand[lastDealerHandIndex].AddComponent<Card>();
        dealerHand[lastDealerHandIndex].GetComponent<Card>().Suit = card.Suit;
        dealerHand[lastDealerHandIndex].GetComponent<Card>().Value = card.Value;
        dealerHand[lastDealerHandIndex].SetActive(true);
        lastDealerHandIndex++;
        //if busted, lose
        List<Card> cardsInHand = new List<Card>();

        foreach (var cardObject in dealerHand)
        {
            if (cardObject.TryGetComponent(out Card cardInHand)) cardsInHand.Add(cardInHand);
        }
        int[] currentDealerHandValues = CardDeck.valueOfHand(cardsInHand);
        blackjackData.intData["DealerHandValue"] = currentDealerHandValues[0];
        blackjackData.intData["DealerHandValueHighAce"] = currentDealerHandValues[1];
        CheckDealerHand();
        UpdateUI();
    }

    public int CheckDealerHand()
    {


        if (blackjackData.intData["DealerHandValue"] > 21 && blackjackData.intData["DealerHandValueHighAce"] > 21)
        {
            return 22;
        }
        else if (blackjackData.intData["DealerHandValue"] == 21 || blackjackData.intData["DealerHandValueHighAce"] == 21)
        {
            //dealer wins no matter what, unless draw
            return 21;
        }
        else if (lastPlayerHandIndex > 4)
        {
            //dealer wins with 5 card charlie
            return 5;
        }
        return -1;
    }

    private void PlayerStart()
    {
        for (int i = 0; i < 2; i++)
        {

            var card = blackjackDeck.DealHand(1)[0];
            playerHand[lastPlayerHandIndex].AddComponent<Card>();
            playerHand[lastPlayerHandIndex].GetComponent<Card>().Suit = card.Suit;
            playerHand[lastPlayerHandIndex].GetComponent<Card>().Value = card.Value;
            playerHand[lastPlayerHandIndex].SetActive(true);
            lastPlayerHandIndex++;
            //if busted, lose
            List<Card> cardsInHand = new List<Card>();

            foreach (var cardObject in playerHand)
            {
                if (cardObject.TryGetComponent(out Card cardInHand)) cardsInHand.Add(cardInHand);
            }
            int[] currentPlayerHandValues = CardDeck.valueOfHand(cardsInHand);
            blackjackData.intData["PlayerHandValue"] = currentPlayerHandValues[0];
            blackjackData.intData["PlayerHandValueHighAce"] = currentPlayerHandValues[1];
            CheckPlayerHand();
            UpdateUI();
        }
    }

    public void FinishGame(int betPayout, bool isDraw = false)
    {
        dealerHand[0].GetComponent<Card>().cardFaceDown = false;
        dealerHand[0].SetActive(false);
        dealerHand[0].SetActive(true);
        if (isDraw)
        {
            GameManager.Instance.gameData.intData["ChipsInHand"] += GameManager.Instance.gameData.intData["ChipsInLimbo"];
            GameManager.Instance.gameData.intData["ChipsInLimbo"] = 0;
            GameManager.Instance.OnWin("Ya matched with the dealear! You got your bet back.");

        }
        else if (betPayout == 0)
        {
            GameManager.Instance.gameData.intData["ChipsInLimbo"] = 0;
            GameManager.Instance.OnLose("Ya busted!");
        }
        else
        {
            GameManager.Instance.gameData.intData["ChipsInHand"] += (betPayout + 1) * GameManager.Instance.gameData.intData["ChipsInLimbo"];
            switch (betPayout)
            {
                case 1:
                    GameManager.Instance.OnWin($"You beat the dealear by not busting! Your payout was 1:1 and you got {(betPayout + 1) * GameManager.Instance.gameData.intData["ChipsInLimbo"]} chips!");
                    break;
                case 2:
                    GameManager.Instance.OnWin($"You beat the dealear by getting Blackjack! Your payout was 2:1 and you got {(betPayout + 1) * GameManager.Instance.gameData.intData["ChipsInLimbo"]} chips!");
                    break;
                case 3:
                    GameManager.Instance.OnWin($"You beat the dealear by hitting a 5 Card Charlie! Your payout was 3:1 and you got {(betPayout + 1) * GameManager.Instance.gameData.intData["ChipsInLimbo"]} chips!");
                    break;
                default:
                    break;
            }
            GameManager.Instance.gameData.intData["ChipsInLimbo"] = 0;
        }
    }

    public void AddBet(int amount)
    {
        if (amount < GameManager.Instance.gameData.intData["ChipsInHand"])
        {
            AudioManager.Instance.PlaySFX(succeedSound);
            GameManager.Instance.gameData.intData["ChipsInLimbo"] += amount;
            GameManager.Instance.gameData.intData["ChipsInHand"] -= amount;
        }
        else
        {
            AudioManager.Instance.PlaySFX(failToSucceedSound);
        }
        UpdateUI();
    }

    public void ClearBet()
    {
        GameManager.Instance.gameData.intData["ChipsInHand"] += GameManager.Instance.gameData.intData["ChipsInLimbo"];
        GameManager.Instance.gameData.intData["ChipsInLimbo"] = 0;
        UpdateUI();
    }

    public void Hit()
    {
        var card = blackjackDeck.DealHand(1)[0];
        playerHand[lastPlayerHandIndex].AddComponent<Card>();
        playerHand[lastPlayerHandIndex].GetComponent<Card>().Suit = card.Suit;
        playerHand[lastPlayerHandIndex].GetComponent<Card>().Value = card.Value;
        playerHand[lastPlayerHandIndex].SetActive(true);
        lastPlayerHandIndex++;
        //if busted, lose
        List<Card> cardsInHand = new List<Card>();

        foreach (var cardObject in playerHand)
        {
            if (cardObject.TryGetComponent(out Card cardInHand)) cardsInHand.Add(cardInHand);
        }
        int[] currentPlayerHandValues = CardDeck.valueOfHand(cardsInHand);
        blackjackData.intData["PlayerHandValue"] = currentPlayerHandValues[0];
        blackjackData.intData["PlayerHandValueHighAce"] = currentPlayerHandValues[1];
        RunDealerTurn();
        CheckPlayerHand();
        UpdateUI();
    }

    public void Stay()
    {

        do { RunDealerTurn(); } while (blackjackData.intData["DealerHandValue"] < 17 || lastDealerHandIndex > 4);

        if (blackjackData.intData["PlayerHandValue"] == blackjackData.intData["DelearHandValue"]) { FinishGame(0, true); return; }
        else
        {
            switch (CheckDealerHand())
            {
                case 22://dealer bust
                    FinishGame(1);
                    return;
                case 21: //dealer blackjack
                    FinishGame(0);
                    return;
                case 5: //dealer five card
                    FinishGame(0);
                    return;
                default:
                    break;
            }
            if (blackjackData.intData["DealerHandValue"] > blackjackData.intData["PlayerHandValue"])
            {
                FinishGame(0);
            }
            else
            {
                FinishGame(1);
            }
        }
    }

    public void Split()
    {

    }

    public void DoubleDown()
    {
        GameManager.Instance.gameData.intData["ChipsInLimbo"] *= 2;
        actionButtons[3].SetActive(false);
        UpdateUI();
    }

    public void ShowMenu()
    {
        GameManager.Instance.pauser.paused = true;
    }

    public void ShowRules()
    {

    }

    private bool CheckPlayerHand()
    {
        if (blackjackData.intData["PlayerHandValue"] > 21 && blackjackData.intData["PlayerHandValueHighAce"] > 21)
        {
            //busted, lose the game
            FinishGame(0);
            return true;
        }
        else if (blackjackData.intData["PlayerHandValue"] == 21 || blackjackData.intData["PlayerHandValueHighAce"] == 21)
        {
            //win game with blackjack
            if (blackjackData.intData["DealerHandValue"] == 21 || blackjackData.intData["DealerHandValueHighAce"] == 21) { FinishGame(0, true); return true; }
            else { FinishGame(2); return true; }
        }
        else if (lastPlayerHandIndex > 4)
        {
            //win game with 5 card charlie
            FinishGame(3);
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        currentBetText.text = $"Current Bet: {GameManager.Instance.gameData.intData["ChipsInLimbo"]}";

    }
}
