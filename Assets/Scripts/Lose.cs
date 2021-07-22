using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    public GameObject panelLose; // Окно "Проигрыш"
    public Text textHighscore; // Набранные очки (на сцене)
    public Text textName; // Имя игрока

    private int highscore; // Набранные очки

    private Score score;
    private TableHighscore tableHighscore;

    private void Awake()
    {
        score = GetComponent<Score>();
        tableHighscore = GetComponent<TableHighscore>();
    }

    public void SetHighscore() // Отображение набранных очков в окне "Проигрыш"
    {
        highscore = score.GetScore();
        textHighscore.text = highscore.ToString();
    }

    public void OnClickNext() // Открытие окна "Таблица рекордов"
    {
        tableHighscore.AddHighscore(highscore, textName.text);

        tableHighscore.panelLiders.SetActive(true);
        panelLose.SetActive(false);
    }
}