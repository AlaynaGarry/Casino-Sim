using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] Image healthUI;
    [SerializeField] TextMeshProUGUI coinUI;
    

    public void UpdateUI()
    {
        healthUI.fillAmount = GameManager.Instance.gameData.intData["Health"] / (float)GameManager.Instance.gameData.intData["MaxHealth"];
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
