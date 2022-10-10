using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenesManager : MonoBehaviour
{
    int levelsQuantity;

    //------------SINGLETON----------

    public static SwitchScenesManager Instance;

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


    //------------SETUP----------

    private void Start()
    {
        levelsQuantity = SceneManager.sceneCountInBuildSettings - 1;
    }


    //------------LEVEL FUNCTIONS----------

    public void LevelUp(int level)
    {
        SceneManager.UnloadSceneAsync(level);
        level++;
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync(levelsQuantity); // last level
        SceneManager.LoadScene(1, LoadSceneMode.Additive); // first level scene
    }

    public void RestartLevel(int level)
    {
        SceneManager.UnloadSceneAsync(level);
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }


    //------------PROPERTIES----------

    public int LevelsQuantity
    {
        get
        {
            return levelsQuantity;
        }
    }
}
