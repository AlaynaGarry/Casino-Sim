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
        deck = new List<Card>();
        discardPile = new List<Card>();
        foreach(cardSuit suit in Enum.GetValues(typeof(cardSuit)))
        {
            foreach (cardValue value in Enum.GetValues(typeof(cardValue)))
            {
                var card = gameObject.AddComponent<Card>();
                card.Suit = suit;
                card.Value = value;
                deck.Add(card);
            }
        }
        ShuffleDeck();
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

    //used in blackjack game, returns an array because i need a vaule with high ace and low ace
    public static int[] valueOfHand(List<Card> handToCheck)
    {
        int[] result = new int[] { 0, 0 };
        foreach(var card in handToCheck)
        {
            if ((int)card.Value == 0)
            {
                result[0] += 1;
                result[1] += 11;
            }
            else
            {
                if((int)card.Value == 12 || (int)card.Value == 11 || (int)card.Value == 10)
                {
                    result[0] += 10;
                    result[1] += 10;
                }
                else
                {
                    result[0] += (int)card.Value + 1;
                    result[1] += (int)card.Value + 1;
                }
            }
        }
        return result;
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
