using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game data
    [SerializeField] bool isGameActive = false;

    [SerializeField] int cherries = 0; // current cherries quantity
    const int maxCherries = 45; // max cherries quantity in this game

    [SerializeField] int gems = 0; // current gems quantity
    const int maxGems = 6; // max gems quantity in this game

    [SerializeField] int currentLevel = 1;

    // Game progress by levels
    Dictionary<int, Dictionary<string, int>> results;

    // Managers
    AudioManager audioManager;
    ManagerUI managerUI;
    SwitchScenesManager switchScenesManager;


    //------------SINGLETON----------

    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    //------------INITIAL SETUP----------

    private void Start()
    {
        audioManager = AudioManager.Instance;
        managerUI = ManagerUI.Instance;
        switchScenesManager = SwitchScenesManager.Instance;

        results = new Dictionary<int, Dictionary<string, int>>();
    }


    //------------GAME WORKFLOW----------

    public void StartGame()
    {
        isGameActive = true;
        managerUI.UpdateGameUI(cherries, gems, currentLevel);
    }

    public void RestartLevel()
    {
        RestoreLevelProgress();
        StartGame();
        switchScenesManager.RestartLevel(currentLevel);
    }

    public void PlayAgain()
    {
        ResetGame();
        switchScenesManager.RestartGame();
    }

    public void ChangeLevel()
    {
        if (currentLevel < 3)
        {
            // Save level progress
            SaveLevelProgress();

            // Switch level
            switchScenesManager.LevelUp(currentLevel);
            currentLevel++;

            // Update UI
            managerUI.UpdateLevelText(currentLevel);
        }
        else
        {
            Win();
        }
    }


    //------------GAME PROGRESS----------

    public void CollectCherry(GameObject cherry)
    {
        cherries++;

        audioManager.PlayCherryCollectedSound();
        managerUI.UpdateCherriresText(cherries);
        cherry.GetComponent<ItemController>().ShowAnimationAndDestroy();
    }

    public void CollectGem(GameObject gem)
    {
        gems++;

        audioManager.PlayGemCollectedSound();
        managerUI.UpdateGemsText(gems);
        gem.GetComponent<ItemController>().ShowAnimationAndDestroy();
    }

    public void GetHome(GameObject character)
    {
        audioManager.PlayLevelUpSound();
        character.GetComponent<Animator>().SetTrigger("home");
    }

    void Win()
    {
        isGameActive = false;
        managerUI.Win(cherries, gems);
    }

    public void Die(GameObject character)
    {
        isGameActive = false;

        audioManager.PlayHurtSound();
        character.GetComponent<Animator>().SetTrigger("die");
    }


    //------HELPERS-------

    void SaveLevelProgress()
    {
        Dictionary<string, int> levelResult = new Dictionary<string, int>();

        levelResult.Add("cherries", cherries);
        levelResult.Add("gems", gems);

        results.Add(currentLevel, levelResult);
    }

    void RestoreLevelProgress()
    {
        int previousLevel = currentLevel - 1;

        cherries = results.ContainsKey(previousLevel) ? results[previousLevel]["cherries"] : 0;
        gems = results.ContainsKey(previousLevel) ? results[previousLevel]["gems"] : 0;
    }

    void ResetGame()
    {
        currentLevel = 1;

        results.Clear();
        RestoreLevelProgress();
        StartGame();
    }


    //------PROPERTIES-------

    public bool IsGameActive
    {
        get
        {
            return isGameActive;
        }
    }

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    public int MaxCherries
    {
        get
        {
            return maxCherries;
        }
    }

    public int MaxGems
    {
        get
        {
            return maxGems;
        }
    }
}
