using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankManager : MonoBehaviour
{
    [SerializeField] TMP_InputField amountEntry;
    [SerializeField] TextMeshProUGUI moneyInBank;
    [SerializeField] TextMeshProUGUI chipsInHand;
    [SerializeField] GameData gameData;

    public void OnEnable()
    {
        UpdateUI();
    }

    public void DepositMoney()
    {
        gameData.intData["ChipsInHand"] -= int.Parse(amountEntry.text);
        gameData.intData["MoneyInBank"] += (gameData.intData["ChipsInHand"] < 0) ? int.Parse(amountEntry.text) + gameData.intData["ChipsInHand"] : int.Parse(amountEntry.text);
        if (gameData.intData["ChipsInHand"] < 0)
        {
            gameData.intData["ChipsInHand"] = 0;
        }
        UpdateUI();
    }


    public void WithdrawChips()
    {
        gameData.intData["MoneyInBank"] -= int.Parse(amountEntry.text);
        gameData.intData["ChipsInHand"] += int.Parse(amountEntry.text);

        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyInBank.text = $"${gameData.intData["MoneyInBank"]}";
        moneyInBank.color = gameData.intData["MoneyInBank"] < 0 ? Color.red : Color.green;

        chipsInHand.text = $"C{gameData.intData["ChipsInHand"]}";
        amountEntry.text = "";
    }
}
