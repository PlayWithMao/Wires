using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Настройки игры")]
    public int amountElements; // Начальное количество блоков в колонке
    public int stepAdditionElements; // Увеличение количества блоков (шаг)
    public int startTime; // Начальное время в секундах
    public int stepTime; // Уменьшение секунд (шаг)

    private int level; // Уровень
    private bool isPlay; // Статус (true = таймер запущен, игра начата)

    private Blocks block;
    private Timer timer;
    private Score score;
    private Lose lose;

    private void Awake()
    {
        block = GetComponent<Blocks>();
        timer = GetComponent<Timer>();
        score = GetComponent<Score>();
        lose = GetComponent<Lose>();
    }

    void Start()
    {
        StartLevel();
    }

    private void StartLevel() // Создание уровня
    {
        block.SetElements(amountElements + stepAdditionElements * level);
        timer.SetTime(startTime - stepTime * level);
        block.CreateBlocks();

        isPlay = true;
    }

    public void RestartLevel() // Перезагрузка уровня
    {
        level = 0;
        score.ClearScore();

        StartLevel();
    }

    public void Winner() // Уровень пройден
    {
        isPlay = false;

        level++;
        СlearTheStage();
        StartLevel();
    }

    private void СlearTheStage() // Очистка уровня
    {
        var objBlock = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < objBlock.Length; i++)
        {
            Destroy(objBlock[i]);
        }

        var objLine = GameObject.FindGameObjectsWithTag("Line");
        for (int i = 0; i < objLine.Length; i++)
        {
            Destroy(objLine[i]);
        }
    }

    public void Losing() // Поражение (время закончилось)
    {
        isPlay = false;

        СlearTheStage();
        lose.SetHighscore();
        lose.panelLose.SetActive(true);
    }

    public bool GetStatusGame()
    {
        return isPlay;
    }
}


