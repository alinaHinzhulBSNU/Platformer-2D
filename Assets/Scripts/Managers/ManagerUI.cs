using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ManagerUI : MonoBehaviour
{
    GameManager gameManager;
    Animator crossfadeAnimator;

    // UI for different game states
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject endUI;
    [SerializeField] GameObject crossfade;

    // UI elements with dynamic text
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI cherriesText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI levelText;


    //------------SINGLETON----------

    public static ManagerUI Instance;

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

        SceneManager.LoadScene(Levels.LEVEL_1, LoadSceneMode.Additive);
    }


    //------------SETUP----------

    void Start()
    {
        gameManager = GameManager.Instance;
        crossfadeAnimator = crossfade.GetComponent<Animator>();
    }


    //------------UPDATE UI----------

    public void UpdateCherriresText(int cherries)
    {
        cherriesText.text = $"Cherries: {cherries}";
    }

    public void UpdateGemsText(int gems)
    {
        gemsText.text = $"Gems: {gems}";
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = $"Level: {level}";
    }

    public void UpdateGameUI(int cherries, int gems, int level)
    {
        UpdateLevelText(level);
        UpdateCherriresText(cherries);
        UpdateGemsText(gems);
    }


    //------------HANDLE GAME EVENTS----------

    public void StartGame()
    {
        startUI.SetActive(false);
        gameUI.SetActive(true);

        gameManager.StartGame();
    }

    public void EndGame()
    {
        gameUI.SetActive(false);
        endUI.SetActive(true);
    }

    public void RestartGame()
    {
        endUI.SetActive(false);
        gameUI.SetActive(true);

        StartCoroutine(ShowTransitionAnimation(gameManager.RestartLevel));
    }

    public void Win(int cherries, int gems)
    {
        gameUI.SetActive(false);

        string text = $"Score: {cherries}/{gameManager.MaxCherries} and {gems}/{gameManager.MaxGems} gems";
        scoreText.text = text;

        winUI.SetActive(true);
    }

    public void PlayAgain()
    {
        winUI.SetActive(false);
        gameUI.SetActive(true);

        StartCoroutine(ShowTransitionAnimation(gameManager.PlayAgain));
    }


    //------------UI ANIMATION----------

    public IEnumerator ShowTransitionAnimation(Action handler)
    {
        // Transition animation
        crossfadeAnimator.SetBool("isStart", true);
        yield return new WaitForSeconds(1.5f);
        crossfadeAnimator.SetBool("isStart", false);
        
        // Handler function from GameManager
        handler();
    }

    public IEnumerator ShowTransitionAnimation(Action<int> handler, int param)
    {
        // Transition animation
        crossfadeAnimator.SetBool("isStart", true);
        yield return new WaitForSeconds(1.5f);
        crossfadeAnimator.SetBool("isStart", false);

        // Handler function from GameManager
        handler(param);
    }
}
