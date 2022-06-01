using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetButtons : MonoBehaviour
{
    [SerializeField] PokerGameManager pokerGameManager;
    [SerializeField] GameObject Btn1;
    [SerializeField] GameObject Btn5;
    [SerializeField] GameObject Btn10;
    [SerializeField] GameObject Btn20;
    [SerializeField] GameObject Btn50;
    [SerializeField] GameObject Btn100;
    [SerializeField] GameObject Btn500;
    [SerializeField] GameObject Btn1000;
    [SerializeField] GameObject Btn5000;
    public bool IsActive = false;

    private int chipAmountAvail = 0;
    private void Update()
    {
        chipAmountAvail = GameManager.Instance.gameData.intData["ChipsInHand"];

        if (IsActive)
        {
            if (chipAmountAvail >= 1) Btn1.SetActive(true);
            else Btn1.SetActive(false);
            if (chipAmountAvail >= 5) Btn5.SetActive(true);
            else Btn5.SetActive(false);
            if (chipAmountAvail >= 10) Btn10.SetActive(true);
            else Btn10.SetActive(false);
            if (chipAmountAvail >= 20) Btn20.SetActive(true);
            else Btn20.SetActive(false);
            if (chipAmountAvail >= 50) Btn50.SetActive(true);
            else Btn50.SetActive(false);
            if (chipAmountAvail >= 100) Btn100.SetActive(true);
            else Btn100.SetActive(false);
            if (chipAmountAvail >= 500) Btn500.SetActive(true);
            else Btn500.SetActive(false);
            if (chipAmountAvail >= 1000) Btn1000.SetActive(true);
            else Btn1000.SetActive(false);
            if (chipAmountAvail >= 5000) Btn5000.SetActive(true);
            else Btn5000.SetActive(false);

        }
        else
        {
            Btn1.SetActive(false);
            Btn5.SetActive(false);
            Btn10.SetActive(false);
            Btn20.SetActive(false);
            Btn50.SetActive(false);
            Btn100.SetActive(false);
            Btn500.SetActive(false);
            Btn1000.SetActive(false);
            Btn5000.SetActive(false);
        }
        
    }
    public void Bet1()
    {
        pokerGameManager.Bet(1);
    }
    public void Bet5()
    {
        pokerGameManager.Bet(5);
    }
    public void Bet10()
    {
        pokerGameManager.Bet(10);
    }
    public void Bet20()
    {
        pokerGameManager.Bet(20);
    }
    public void Bet50()
    {
        pokerGameManager.Bet(50);
    }
    public void Bet100()
    {
        pokerGameManager.Bet(100);
    }
    public void Bet500()
    {
        pokerGameManager.Bet(500);
    }
    public void Bet1k()
    {
        pokerGameManager.Bet(1000);
    }
    public void Bet5k()
    {
        pokerGameManager.Bet(5000);
    }
}
