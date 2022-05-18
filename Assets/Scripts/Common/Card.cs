using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public cardSuit Suit;
    public cardValue Value;
    public bool onBoard = false;
    public bool hold = false;
    public bool cardFaceDown = false;
    public Sprite cardImage;


    private void OnEnable()
    {
        switch (Value)
        {
            case cardValue.ACE:
                SetCard(1);
                break;
            case cardValue.TWO:
                SetCard(2);
                break;
            case cardValue.THREE:
                SetCard(3);
                break;
            case cardValue.FOUR:
                SetCard(4);
                break;
            case cardValue.FIVE:
                SetCard(5);
                break;
            case cardValue.SIX:
                SetCard(6);
                break;
            case cardValue.SEVEN:
                SetCard(7);
                break;
            case cardValue.EIGHT:
                SetCard(8);
                break;
            case cardValue.NINE:
                SetCard(9);
                break;
            case cardValue.TEN:
                SetCard(10);
                break;
            case cardValue.JACK:
                SetCard(11);
                break;
            case cardValue.QUEEN:
                SetCard(12);
                break;
            case cardValue.KING:
                SetCard(13);
                break;
            default:
                break;
        }
    }

    public void SetCard(int value)
    {
        int additiveForList = 0;
        switch (Suit)
        {
            case cardSuit.DIAMOND:
                additiveForList = 13;
                break;
            case cardSuit.HEART:
                additiveForList = 26;
                break;
            case cardSuit.SPADES:
                additiveForList = 39;
                break;
            case cardSuit.CLUBS:
                additiveForList = 0;
                break;
            default:
                break;
        }
        if (TryGetComponent(out Image image))
            if (cardFaceDown) image.sprite = GameManager.Instance.cardImages[GameManager.Instance.cardImages.Count - 1];
            else
                image.sprite = GameManager.Instance.cardImages[(value - 1) + additiveForList];
    }

    public Card()
    {

    }

    public Card(cardSuit suit, cardValue value)
    {
        Suit = suit;
        Value = value;
    }
}
