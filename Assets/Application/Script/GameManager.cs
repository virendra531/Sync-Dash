using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CrashEffect crashEffect;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        // Time.timeScale = 0; // Pause the game
        crashEffect.TriggerCrashEffect();
        StartCoroutine(ShakeCamera());
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ShakeCamera()
    {
        Vector3 originalPos = Camera.main.transform.position;
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            Camera.main.transform.position = originalPos + Random.insideUnitSphere * 0.1f;
            yield return null;
        }
        Camera.main.transform.position = originalPos;
    }
}