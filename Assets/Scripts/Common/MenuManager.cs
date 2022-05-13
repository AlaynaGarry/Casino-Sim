using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject pause;
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;

    private void Start()
    {
        master.onValueChanged.AddListener(delegate { changeMaster(); });
        music.onValueChanged.AddListener(delegate { changeMusic(); });
        sfx.onValueChanged.AddListener(delegate { changeSFX(); });
        SceneManager.sceneLoaded += (delegate { EnsureAllUIOff(); }) ;
    }

    public void GoFromCanvasToCanvas(GameObject canvasFrom, GameObject canvasTo)
    {
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut(.25f);
        StartCoroutine(FromCanvasToCanvas(canvasFrom, canvasTo));
    }

    IEnumerator FromCanvasToCanvas(GameObject canvasFrom, GameObject canvasTo)
    {
        yield return new WaitUntil(() => GameManager.instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeIn(.25f);

        canvasFrom.SetActive(false);
        canvasTo.SetActive(true);
    }

    public void SeeSettings()
    {
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut(.25f);
        master.value = AudioManager.Instance.masterVolume;
        music.value = AudioManager.Instance.musicVolume;
        sfx.value = AudioManager.Instance.sfxVolume;
        StartCoroutine(GoToSettings());
    }
    public void SeePause()
    {
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut(.25f);
        StartCoroutine(GoToPause());
    }

    public void SeeCredits()
    {
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut();
        StartCoroutine(GoToCredits());
    }
    public void SelectLevel(string sceneName)
    {
        GameManager.Instance.pauser.canPause = true;
        GameManager.Instance.OnLoadScene(sceneName);
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut();
        StartCoroutine(FadeToReload());
    }
    public void QuitGame()
    {

        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut();
        StartCoroutine(FadeToQuit());
    }

    public void BackFromSettings()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene != "MainMenu")
        {
            SeePause();
        }
        else
        {
            GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeOut(.25f);
            StartCoroutine(BackToMainMenu());
        }
    }

    IEnumerator BackToMainMenu()
    {
        yield return new WaitUntil(() => GameManager.instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeIn(.25f);
        settings.SetActive(false);
    }

    IEnumerator GoToCredits()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeIn();
        
        credits.SetActive(true);
    }

    IEnumerator GoToSettings()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeIn(.25f);
        if (pause) pause.SetActive(false);
        settings.SetActive(true);
    }

    IEnumerator GoToPause()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.gameObject.GetComponent<ScreenFade>().FadeIn(.25f);

        settings.SetActive(false);
        pause.SetActive(true);
    }
   
    IEnumerator FadeToReload()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameObject.GetComponent<ScreenFade>().isDone);
        GameManager.Instance.OnLoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator FadeToQuit()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameObject.GetComponent<ScreenFade>().isDone);
        Application.Quit();
        print("quit");
    }


    public void changeMaster()
    {
        AudioManager.Instance.masterVolume = master.value;
    }

    public void changeSFX()
    {
        AudioManager.Instance.sfxVolume = sfx.value;
    }

    public void changeMusic()
    {
        AudioManager.Instance.musicVolume = music.value;
    }

    private void EnsureAllUIOff()
    {
        pause?.SetActive(false);
        settings?.SetActive(false);
        if(credits) credits.SetActive(false);
    }
}
