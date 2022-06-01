using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] Transform holdUI;
    public bool readyToCheck = false;
    private Card card;
    public float cardTransform = 0;

    private void Awake()
    {
        if(cardTransform == 0) cardTransform = gameObject.transform.position.y - holdUI.position.y;
    }

    public void CardClick()
    {
        if (!readyToCheck)
        {
            if(card == null) card = gameObject.GetComponent<Card>();
            card.hold = !card.hold;

            if (card.hold) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - cardTransform, gameObject.transform.position.z);
            else gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + cardTransform, gameObject.transform.position.z);
        }
    }
}
