using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public cardSuit Suit;
    public cardValue Value;
    public bool onBoard = false;
    public bool hold = false;
    public bool cardFaceDown = true;
    public Sprite cardImage;


    private void OnEnable()
    {
        switch (Value)
        {
            case cardValue.ACE:
                break;
            case cardValue.TWO:
                break;
            case cardValue.THREE:
                break;
            case cardValue.FOUR:
                break;
            case cardValue.FIVE:
                break;
            case cardValue.SIX:
                break;
            case cardValue.SEVEN:
                break;
            case cardValue.EIGHT:
                break;
            case cardValue.NINE:
                break;
            case cardValue.TEN:
                break;
            case cardValue.JACK:
                break;
            case cardValue.QUEEN:
                break;
            case cardValue.KING:
                break;
            default:
                break;
        }
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
