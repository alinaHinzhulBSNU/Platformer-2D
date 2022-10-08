using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game data
    [SerializeField] bool isGameActive = false;

    [SerializeField] int cherries = 0;
    [SerializeField] int gems = 0;

    [SerializeField] int currentLevel = 1;

    // Managers
    AudioManager audioManager;


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
    }


    //------------GAME PROGRESS----------

    public void LevelUp(GameObject character)
    {
        currentLevel++;

        audioManager.PlayLevelUpSound();
        character.GetComponent<Animator>().SetTrigger("home");
    }

    public void GameOver(GameObject character)
    {
        isGameActive = false;

        audioManager.PlayHurtSound();
        character.GetComponent<Animator>().SetTrigger("die");
    }


    //------------CONTROL GAME----------

    public void CollectCherry(GameObject cherry)
    {
        cherries++;

        audioManager.PlayCherryCollectedSound();
        cherry.GetComponent<ItemController>().ShowAnimationAndDestroy();
    }

    public void CollectGem(GameObject gem)
    {
        gems++;

        audioManager.PlayGemCollectedSound();
        gem.GetComponent<ItemController>().ShowAnimationAndDestroy();
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
}
