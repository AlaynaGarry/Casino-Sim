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
        GAME_OVER,
        GAME_WIN,
        GAME_WAIT
    }

    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject winUI;
    [SerializeField] TextMeshProUGUI winUIMessage;
    [SerializeField] GameObject loseUI;
    [SerializeField] AudioClip loseMusic;
    [SerializeField] AudioClip winMusic;
    public List<Sprite> cardImages = new List<Sprite>();



    public Pause pauser;
    public GameData gameData;

    public State state = State.TITLE;

    public override void Awake()
    {
        base.Awake();       

        SceneManager.activeSceneChanged += OnSceneWasLoaded;
        SceneManager.sceneLoaded += (delegate { EnsureAllUIOff(); });
    }

    private void Start()
    {
        InitScene();
    }

    void InitScene()
    {        
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
                if (gameData.intData["Chips"] <= 0 && gameData.intData["ChipsInLimbo"] == 0)
                {
                    state = State.GAME_OVER;
                    OnLose();
                }
                break;
            case State.GAME_OVER:
                break;
            case State.GAME_WIN:
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

    void OnSceneWasLoaded(Scene current, Scene next)
    {
        InitScene();
    }

    public void RestartGame()
    {
        
    }

    public int[] GetRandomResult(int minInclusive, int maxInclusive, int numOfResults = 1)
    {
        if (numOfResults <= 0) return new int[0];
        int[] resultingArray = new int[numOfResults];
        for (int resultIndex = 0; resultIndex < resultingArray.Length; resultIndex++)
        {
            resultingArray[resultIndex] = Random.Range(minInclusive, maxInclusive);
        }
        return resultingArray;
    }


    public void OnLose()
    {
        loseUI.SetActive(true);
        pauser.paused = false;
        if (loseMusic) AudioManager.Instance.PlayMusic(loseMusic);
    }

    public void OnWin(string messageToDisplay)
    {
        state = State.GAME_WIN;
        pauser.paused = false;
        if (winMusic) AudioManager.Instance.PlayMusic(winMusic);
        winUI.SetActive(true);
        winUIMessage.text = messageToDisplay;
    }

    private void EnsureAllUIOff()
    {
        winUI?.SetActive(false);
        loseUI?.SetActive(false);
    }
}
