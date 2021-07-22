using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text textScore; // Очки (на сцене)

    private int score; // Очки

    private Timer timer;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    public void AddScore() // Добавление очков
    {
        score += 10 * timer.GetTime();
        UpdateScore();
    }

    private void UpdateScore() // Обновление очков на сцене
    {
        textScore.text = score.ToString();
    }

    public void ClearScore()  // Обнулись очки
    {
        score = 0;
        UpdateScore();
    }

    public int GetScore()
    {
        return score;
    }
}
