using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PokerGameManager : MonoBehaviour
{
    [SerializeField] GameObject AnteButton;
    [SerializeField] GameObject GoodUI;
    [SerializeField] GameObject GameIntroUI;
    [SerializeField] GameObject CardHandsExamplesUI;
    [SerializeField] GameObject DeckUI;
    [SerializeField] GameObject DiscardUI;
    [SerializeField] GameObject HoldBtnUI;
    [SerializeField] GameObject CheckHandBtnUI;
    [SerializeField] GameObject HandEXBtnUI;
    [SerializeField] BetButtons BetButtonsUI;
    [SerializeField] CardDeck cardDeck;
    [SerializeField] List<GameObject> PlayerHandUI = new List<GameObject>();
    [SerializeField] int ante = 10;
    [SerializeField] TextMeshProUGUI betAmountText;
    [SerializeField] TextMeshProUGUI playerMoneyText;
    public enum State
    {
        PRE_GAME,
        CREATE_DECK,
        GAME_SET_UP,
        GAME,
        CHECK_HAND,
        RESET,
        GAME_OVER,
        NONE
    }

    public enum WinRate : int
    {
        ROYALFLUSH = 100000,
        STRAIGHTFLUSH = 10000,
        FOUROFAKIND = 1000,
        FULLHOUSE = 100,
        FLUSH = 50,
        STRAIGHT = 25,
        THREEOFAKIND = 10,
        TWOPAIR = 5,
        ONEPAIR = 1,
        HIGHCARD = 0,
        None = 0
    }

    public int playerMoney = 0;
    public int moneyInPool = 0;
    private State state = State.PRE_GAME;
    private WinRate winRate = WinRate.None;
    private int lastPlayerHandIndex = 0;

    public object HandEXUI { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
        int winning = 1 * ((int)WinRate.FLUSH);
    }

    // Update is called once per frame
    void Update()
    {
        moneyInPool = GameManager.Instance.gameData.intData["ChipsInLimbo"];
        betAmountText.text = $"{moneyInPool}.00";
        playerMoney = GameManager.Instance.gameData.intData["ChipsInHand"];
        playerMoneyText.text = $"Wallet: {playerMoney}.00";

        switch (state)
        {
            case State.PRE_GAME:
                BetButtonsUI.IsActive = false;
                GoodUI.SetActive(true);
                GameIntroUI.SetActive(true);
                //ACTIVATE WELCOME UI WITH SIMPLE TUTORIAL
                //ONCE THEY CLICK PLAY, SWITCH STATES TO CREATE DECK
                break;
            case State.CREATE_DECK:
                CreateDeck();

                state = State.GAME_SET_UP;
                // CREATE DECK
                //SWITCH STATE TO GAME_SETUP
                break;
            case State.GAME_SET_UP:

                HandEXBtnUI.SetActive(true);
                winRate = WinRate.None;
                AnteButton.SetActive(true);

                //PAY ANTE
                //adds bet to ante
                //SHUFFLE CARDS

                //DEAL 
                break;
            case State.GAME:
                if (cardDeck.discardPile.Count > 5) DiscardUI.SetActive(true);
                else DiscardUI.SetActive(false);
                
                //if statement waiting for hold or bet or draw
                //draw and hold both end current play
                //draw discards chosen cards if Hold = true
                //bet lets player bet money 
                //add bet to the pool
                //switch state to check hand;
                break;
            case State.CHECK_HAND:
                winRate = CheckHighestHand();
                state = State.GAME_OVER;
                //winLose() - pull up win or lose screen for player
                    //if win - show player their winnings
                    //both screens have continu button that sets state depending if they can play again
                //if( player still had chips ) - state = reset
                //if( player doesnt chips ) - state = gameover
                break;
            case State.RESET:
                //if statements and GUI asking if they want to play again
                    //if yes
                        //reset deck
                        //reset player hand
                        //reset ante
                        //switch state = gamesetup
                    //if no
                        //switch state to game over
                
                break;
            case State.GAME_OVER:
                WinLose();
                //game over ui pulls up with message with continue
                    //if no chips - message = no more chips
                    //if player has chips - thanks for playing
                //game switches to main menu once they click continue
                break;
            case State.NONE:
                //game over ui pulls up with message with continue
                //if no chips - message = no more chips
                //if player has chips - thanks for playing
                //game switches to main menu once they click continue
                break;
            default:
                break;
        }
    }

    public void CreateDeck()
    {
        cardDeck.CreateDeck();
        DeckUI.SetActive(true);
    }
    public void DealPlayerDeck()
    {
        List<Card> playerHand = new List<Card>();
        playerHand = cardDeck.DealHand(5);

        for (int i = 0; i < 5; i++)
        {
            var card = playerHand[i];
            PlayerHandUI[lastPlayerHandIndex].AddComponent<Card>();
            PlayerHandUI[lastPlayerHandIndex].GetComponent<Card>().Suit = card.Suit;
            PlayerHandUI[lastPlayerHandIndex].GetComponent<Card>().Value = card.Value;
            PlayerHandUI[lastPlayerHandIndex].SetActive(true);
            lastPlayerHandIndex++;
        }
    }
    public void PayAnte()
    {
        AnteButton.SetActive(false);
        HoldBtnUI.SetActive(true);
        BetButtonsUI.IsActive = true;
        GameManager.Instance.gameData.intData["ChipsInHand"] -= 10;
        GameManager.Instance.gameData.intData["ChipsInLimbo"] += 10;
        GameSetup();
        //find way to get that amount from chips player has
    }
    public void GameSetup()
    {
        cardDeck.ShuffleDeck();
        DealPlayerDeck();
//need to reveal hand
        state = State.GAME;
    }

    public void Bet(int val)
    {
        GameManager.Instance.gameData.intData["ChipsInHand"] -= val;
        GameManager.Instance.gameData.intData["ChipsInLimbo"] += val;
    }

    public void DiscardCards()
    {

        HoldBtnUI.SetActive(false);
        CheckHandBtnUI.SetActive(true);
        BetButtonsUI.IsActive = false;

        int count = 0;
        foreach(var card in PlayerHandUI)
        {
            card.GetComponent<CardUI>().readyToCheck = true;
            if(!card.GetComponent<Card>().hold)
            {
                count++;
            }
            else card.gameObject.transform.position = new Vector3(card.gameObject.transform.position.x, card.gameObject.transform.position.y + card.GetComponent<CardUI>().cardTransform, card.gameObject.transform.position.z);
        }

        if(count > 0)
        {
            List<Card> cardsToDeal = new List<Card>();

            cardsToDeal = cardDeck.DealHand(count);

            int index = 0;
            
            foreach (var item in PlayerHandUI)
            {
                if (!item.GetComponent<Card>().hold)
                {
                    item.GetComponent<Card>().Value = cardsToDeal[index].Value;
                    item.GetComponent<Card>().Suit = cardsToDeal[index].Suit;
                    index++;
                    item.SetActive(false);
                    item.SetActive(true);
                }
            }
        } 
    }

    public void WinLose()
    {
        if(winRate == WinRate.HIGHCARD)
        {
            GameManager.Instance.OnLose("You had JUNK Cards, Try Again or Quit!");
        }
        else
        {
            GameManager.Instance.gameData.intData["ChipsInHand"] += (moneyInPool + (moneyInPool * ((int)winRate)));
            GameManager.Instance.OnWin("Your hand was a " + winRate.ToString() + ". \nCongradulations!!! You won " + (moneyInPool + (moneyInPool * ((int)winRate))));
        }
        GameManager.Instance.gameData.intData["ChipsInLimbo"] = 0;

        state = State.NONE;
    }

    public WinRate CheckHighestHand()
    {
        WinRate returnRate = WinRate.None;
        //get pattern if flush
        WinRate suitCheck = CheckSuit();
        //check pattern if straight
        WinRate inOrderCheck = CheckOrder();

        //if both are true, return straightflush
        if (suitCheck == WinRate.FLUSH && inOrderCheck == WinRate.STRAIGHT) return WinRate.STRAIGHTFLUSH;
        //if true, return flush
        else if (suitCheck == WinRate.FLUSH) return suitCheck;
        //if true, return straight
        else if (inOrderCheck == WinRate.STRAIGHT) return inOrderCheck;

        //if it gets here, check for pairs or other hands
        return CheckValue();
    }

    public WinRate CheckOrder()
    {
        List<int> numList = new List<int>();

        foreach (var c in PlayerHandUI)
        {
            numList.Add(((int)c.GetComponent<Card>().Value) + 1);
        }

        numList.Sort();
        int tempNum = numList[0];

        foreach(int num in numList)
        {
            if (num != tempNum) return WinRate.None;
            tempNum++;
        }

        return WinRate.STRAIGHT;
    }

    public WinRate CheckSuit()
    {
        List<string> enumList = new List<string>();
        List<int> uniqueEnums = new List<int>();
        string cardHandPattern = "";

        foreach (var c in PlayerHandUI)
        {
            Card card = c.GetComponent<Card>();
            if (!enumList.Contains(card.Suit.ToString()))
            {
                uniqueEnums.Add(PlayerHandUI.Where(ch => ch.GetComponent<Card>().Suit == card.Suit).Count());
                enumList.Add(card.Suit.ToString());
            }
        }

        uniqueEnums.Sort();
        foreach (int num in uniqueEnums) cardHandPattern += num;
        //return winrate.flush if a flush
        //else return none
        return WinRate.None;
    }

    public WinRate CheckValue()
    {
        //write algo to look for unique characters to find pairs trio or quads
        List<string> enumList = new List<string>();
        List<int> uniqueEnums = new List<int>();
        string cardHandPattern = "";

        foreach (var c in PlayerHandUI)
        {
            Card card = c.GetComponent<Card>();
            if (!enumList.Contains(card.Value.ToString()))
            {
                uniqueEnums.Add(PlayerHandUI.Where(ch => ch.GetComponent<Card>().Value == card.Value).Count());
                enumList.Add(card.Value.ToString());
            }
        }

        uniqueEnums.Sort();
        foreach (int num in uniqueEnums) cardHandPattern += num;

        if (cardHandPattern == "11111") return WinRate.HIGHCARD;
        else if (cardHandPattern == "1112") return WinRate.ONEPAIR;
        else if (cardHandPattern == "122") return WinRate.TWOPAIR;
        else if (cardHandPattern == "113") return WinRate.THREEOFAKIND;
        else if (cardHandPattern == "23") return WinRate.FULLHOUSE;
        else if (cardHandPattern == "14") return WinRate.FOUROFAKIND;

        return WinRate.None;
    }

    public void PlayGame()
    {
        GoodUI.SetActive(false);
        GameIntroUI.SetActive(false);
        cardDeck.ShuffleDeck();

        state = State.CREATE_DECK;
    }

    public void PlayAgain()
    {
        GoodUI.SetActive(false);

        state = State.GAME_SET_UP;
    }

    public void HandsEX()
    {
        GoodUI.SetActive(true);
        CardHandsExamplesUI.SetActive(true);
        HandEXBtnUI.SetActive(false);
    }
    public void CloseExamples()
    {
        GoodUI.SetActive(false);
        CardHandsExamplesUI.SetActive(false);
        HandEXBtnUI.SetActive(true);
    }
    public void CheckHandClick()
    {
        CheckHandBtnUI.SetActive(false);
        state = State.CHECK_HAND;
    }
}
