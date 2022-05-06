using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        TITLE,
        PLAYER_START,
        GAME,
        PLAYER_DEAD,
        GAME_OVER,
        GAME_WIN,
        GAME_WAIT
    }

    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] AudioClip loseMusic;
    [SerializeField] AudioClip winMusic;

    public Pause pauser;
    public GameData gameData;

    public State state = State.TITLE;
    int highScore;

    public override void Awake()
    {
        base.Awake();

        gameData.intData["Score"] = 0;

        

        SceneManager.activeSceneChanged += OnSceneWasLoaded;
    }

    private void Start()
    {
        InitScene();
    }
    void InitScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1")
        {
            state = State.GAME;
            Restart();
        }
        else if (sceneName == "MainMenu")
        {
            state = State.TITLE;
            winUI.SetActive(false);
            loseUI.SetActive(false);
        }
        SceneDescriptor sceneDescriptor = FindObjectOfType<SceneDescriptor>();
        if (sceneDescriptor != null)
        {
            if (sceneDescriptor.player) Instantiate(sceneDescriptor.player, sceneDescriptor.playerSpawn.position, sceneDescriptor.playerSpawn.rotation);
            if (sceneDescriptor.music) AudioManager.Instance.PlayMusic(sceneDescriptor.music);
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.TITLE:
                break;
            case State.PLAYER_START:
                break;
            case State.GAME:
                if (gameData.intData["Score"] >= gameData.intData["ScoreToWin"])
                {
                    AudioManager.Instance.PlayMusic(winMusic);
                    state = State.GAME_WIN;
                }else if(gameData.floatData["TimeRemaing"] <= 0)
                {
                    state = State.PLAYER_DEAD;
                }
                break;
            case State.PLAYER_DEAD:
                AudioManager.Instance.PlayMusic(loseMusic);
                state = State.GAME_OVER;
                break;
            case State.GAME_OVER:
                Time.timeScale = 0;
                loseUI.SetActive(true);
                state = State.GAME_WAIT;
                break;
            case State.GAME_WIN:
                Time.timeScale = 0;
                winUI.SetActive(true);
                state = State.GAME_WAIT;
                break;
            case State.GAME_WAIT:
                break;
            default:
                break;
        }
    }

    public void OnLoadScene(string sceneName)
    {
        sceneLoader.Load(sceneName);
    }

    public void OnPlayerDead()
    {
        state = State.PLAYER_DEAD;
    }

    void OnSceneWasLoaded(Scene current, Scene next)
    {
        InitScene();
    }

    public void Restart()
    {
        gameData.intData["Score"] = 0;
        gameData.intData["Health"] = gameData.intData["MaxHealth"];
    }
}
