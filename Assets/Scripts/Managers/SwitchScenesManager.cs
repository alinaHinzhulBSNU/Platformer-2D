using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenesManager : MonoBehaviour
{
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


    //------------LEVEL FUNCTIONS----------

    public void LevelUp(int level)
    {
        SceneManager.UnloadSceneAsync(GetSceneNameByLevel(level));
        level++;
        SceneManager.LoadScene(GetSceneNameByLevel(level), LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync(Levels.LEVEL_3); // last level
        SceneManager.LoadScene(Levels.LEVEL_1, LoadSceneMode.Additive); // first level
    }

    public void RestartLevel(int level)
    {
        SceneManager.UnloadSceneAsync(GetSceneNameByLevel(level));
        SceneManager.LoadScene(GetSceneNameByLevel(level), LoadSceneMode.Additive);
    }

    string GetSceneNameByLevel(int level)
    {
        switch (level)
        {
            case 1:
                return Levels.LEVEL_1;
            case 2:
                return Levels.LEVEL_2;
            case 3:
                return Levels.LEVEL_3;
            default:
                return null;
        }
    }
}
