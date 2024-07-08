using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverText;

    static public GameManager instance;

    private void Start()
    {
        instance = this;
    }

    public void GameOver(int score)
    {
        gameOverText.SetActive(true);
        // display score in UI
        gameOverText.GetComponent<TextMeshProUGUI>().text += score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}