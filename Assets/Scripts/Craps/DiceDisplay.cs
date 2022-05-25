using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite num1;
    [SerializeField] Sprite num2;
    [SerializeField] Sprite num3;
    [SerializeField] Sprite num4;
    [SerializeField] Sprite num5;
    [SerializeField] Sprite num6;

    public void changeImage(int rollValue)
    {
        if(rollValue == 1)
        {
            spriteRenderer.sprite = num1;
        }
        else if(rollValue == 2)
        {
            spriteRenderer.sprite = num2;
        }
        else if (rollValue == 3)
        {
            spriteRenderer.sprite = num3;
        }
        else if (rollValue == 4)
        {
            spriteRenderer.sprite = num4;
        }
        else if (rollValue == 5)
        {
            spriteRenderer.sprite = num5;
        }
        else
        {
            spriteRenderer.sprite = num6;
        }
    }
}
