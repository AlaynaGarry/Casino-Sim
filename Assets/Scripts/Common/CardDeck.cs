using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public void CreateDeck()
    {
        foreach(cardSuit suit in Enum.GetValues(typeof(cardSuit)))
        {
            foreach (cardValue value in Enum.GetValues(typeof(cardValue)))
            {
                deck.Add(new Card(suit, value));
            }
        }
    }

    public void ShuffleDeck()
    {
        //List<Card> temp = deck;
        var rnd = new System.Random();

        if(discardPile != null)
        {
            foreach(var card in discardPile)
            {
                deck.Add(card);
            }
        }

        deck = deck.OrderBy(item => rnd.Next()).ToList();
    }

    public List<Card> DealHand(int numOfCards)
    {
        List<Card> hand = new List<Card>();

        for (int i = 0; i < numOfCards; i++)
        {
            hand.Add(deck[0]);
            discardPile.Add(deck[0]);
            deck.Remove(deck[0]);
        }

        return hand;
    }

    public void Discard(List<Card> cards)
    {
        if (cards == null) return;

        foreach(Card card in cards)
        {
            discardPile.Add(card);
        }
    }
}

public enum cardValue
{
    ACE,
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK,
    QUEEN,
    KING
}

public enum cardSuit
{
    DIAMOND,
    HEART,
    SPADES,
    CLUBS
}
