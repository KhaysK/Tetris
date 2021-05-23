using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text bestScoreText;
    public Text highScoreText;

    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject Spawner;

    public int score;

    // Вызывается при старте сцены 
    public void Start()
    {
        Time.timeScale = 1;
    }

    // Работает как бесконечный цикл 
    void Update()
    {
        scoreText.text = score.ToString();

        highScoreText.text = PlayerPrefs.GetInt("Record").ToString();
        bestScoreText.text = PlayerPrefs.GetInt("Record").ToString();

        if (score > PlayerPrefs.GetInt("Record")) PlayerPrefs.SetInt("Record", score);
    }

    public void Pause()
    {
        // Ставит игру на паузу
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Play()
    {
        // Снимает игру с паузы
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    public void ResetLvl()
    {
        // Перезагружает уровень
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        // Загружает сцену с меню
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        // Конец игры
        GameOverPanel.SetActive(true);
        Spawner.SetActive(false);
        Time.timeScale = 0;
    }
}
