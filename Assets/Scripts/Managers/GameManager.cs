using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    [SerializeField] int currentLevel = 1;

    [SerializeField] int cherries = 0;
    [SerializeField] int gems = 0;

    [SerializeField] bool isGameActive = true;


    //------------GAME PROGRESS----------

    public void LevelUp()
    {
        currentLevel++;

        audioManager.PlayLevelUpSound(); // sound
    }

    public void GameOver(GameObject character)
    {
        isGameActive = false;

        character.GetComponent<Animator>().SetTrigger("die"); // animation
        audioManager.PlayHurtSound(); // sound
    }


    //------------CONTROL GAME----------

    public void CollectCherry(GameObject cherry)
    {
        cherries++;

        cherry.GetComponent<ItemController>().ShowAnimationAndDestroy();
        audioManager.PlayCherryCollectedSound();
    }

    public void CollectGem(GameObject gem)
    {
        gems++;

        gem.GetComponent<ItemController>().ShowAnimationAndDestroy();
        audioManager.PlayGemCollectedSound();
    }


    //------PROPERTIES-------

    public bool IsGameActive
    {
        get
        {
            return isGameActive;
        }
    }
}
