using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject infoPanel;
    public void Info()
    {
        // Отображает и скрывает панель с информацией
        infoPanel.SetActive(!infoPanel.activeSelf);
    }

    public void StartLvl()
    {
        // Загружает сцену с игрой
        SceneManager.LoadScene(1);
    }
}
