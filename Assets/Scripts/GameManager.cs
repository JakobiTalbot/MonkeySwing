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
    private float score = 0;
    private float lastXPos;

    private void Start()
    {
        lastXPos = transform.position.x;
        if (!instance)
            instance = this;
    }

    private void LateUpdate()
    {
        float xPosDelta = transform.position.x - lastXPos;
        if (xPosDelta > 0f)
            score += xPosDelta;

        lastXPos = transform.position.x;
    }

    public void GameOver(int distance)
    {
        gameOverText.SetActive(true);
        score += distance;
        // display score in UI
        gameOverText.GetComponent<TextMeshProUGUI>().text += ((int)score).ToString();
    }

    public void RestartGame() => SceneManager.LoadScene(0);
    public void AddScore(int increaseToScore) => score += increaseToScore;
}