using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;//0
    [SerializeField] GameObject bank;//1
    [SerializeField] GameObject gameSelect;//2
    [SerializeField] GameObject credits;//3

    public void ToMenu(int canvasNum)
    {
        switch (canvasNum)
        {
            case 1:
                MenuManager.Instance.GoFromCanvasToCanvas(bank, mainMenu);
                break;
            case 2:
                MenuManager.Instance.GoFromCanvasToCanvas(gameSelect, mainMenu);
                break;
            case 3:
                MenuManager.Instance.GoFromCanvasToCanvas(credits, mainMenu);
                break;
            default:
                break;
        }
    }

    public void LoadScene(string name)
    {
        GameManager.Instance.OnLoadScene(name);
    }

    public void ToBank()
    {
        MenuManager.Instance.GoFromCanvasToCanvas(mainMenu, bank);
    }

    public void ToCredits()
    {
        MenuManager.Instance.GoFromCanvasToCanvas(mainMenu, credits);
    }

    public void ToGameSelect()
    {
        MenuManager.Instance.GoFromCanvasToCanvas(mainMenu, gameSelect);

    }

    public void QuitGame()
    {
        MenuManager.Instance.QuitGame();
    }

    public void ToSettings()
    {
        MenuManager.Instance.SeeSettings();
    }
}
