using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerGameManager : MonoBehaviour
{
    [SerializeField] CardDeck cardDeck;
    [SerializeField] InputField betInput;
    [SerializeField] int ante = 10;

    public enum State
    {
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
        ROYALFLUSH = 1,
        STRAIGHTFLUSH = 1,
        FOUROFAKIND = 1,
        FULLHOUSE = 1,
        FLUSH = 1,
        STRAIGHT = 1,
        THREEOFAKIND = 1,
        TWOPAIR = 1,
        ONEPAIR = 1,
        HIGHCARD = 1
    }

    private State state = State.CREATE_DECK;
    private int moneyInPool = 0;
    private List<Card> playerHand = new List<Card>();
    private List<Card> houseHand = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        int winning = 1 * ((int)WinRate.FLUSH);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.CREATE_DECK:
                break;
            case State.GAME_SET_UP:
                break;
            case State.GAME:
                break;
            case State.CHECK_HAND:
                break;
            case State.RESET:
                break;
            case State.GAME_OVER:
                break;
            default:
                break;
        }
    }

    public void CreateDeck()
    {
        cardDeck.CreateDeck();
    }
    public void PayAnte()
    {
        moneyInPool += ante;
        //find way to get that amount from chips player has
    }

    public void GameSetUp()
    {
        cardDeck.ShuffleDeck();
        playerHand = cardDeck.DealHand(5);
        houseHand = cardDeck.DealHand(5);
    }


    public void Call()
    {

    }

    public void Bet()
    {

    }

    public void DiscardCards()
    {
        List<Card> cardsToDiscard = new List<Card>();
        foreach(Card card in playerHand)
        {
            if(card.hold == false)
            {
                cardsToDiscard.Add(card);
                playerHand.Remove(card);
            }
        }

        if(cardsToDiscard != null)
        {
            List<Card> cardsToDeal = new List<Card>();
            
            cardDeck.Discard(cardsToDiscard);
            cardsToDeal = cardDeck.DealHand(5 - playerHand.Count);

            foreach(Card card in cardsToDeal)
            {
                playerHand.Add(card);
            }
        }
        
        state = State.CHECK_HAND;
    }

    public void WinLose()
    {

    }

    public bool CheckHand()
    {
        return false;
    }

    public void ClearBoard()
    {

    }
}
