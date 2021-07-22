using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text textTime; // Время в секундах (на сцене)

    private int time; // Время в секундах
    private float miliSecond; // Время в миллисекундах

    private GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        if (gameController.GetStatusGame()) Countdown();
    }

    private void Countdown() // Таймер
    {
        miliSecond += 0.02f;
        if (miliSecond >= 1)
        {
            ReduceTime();
            miliSecond = 0;
        }
    }

    private void ReduceTime() // Уменьшение времени
    {
        time--;
        UpdateTime();
        DefeatCheck();
    }

    private void UpdateTime() // Обновление времени на сцене
    {
        textTime.text = time.ToString();
    }

    private void DefeatCheck() // Проверка на поражение
    {
        if (time == 0) gameController.Losing();
    }

    public void SetTime(int t)
    {
        time = t;
        miliSecond = 0;
        UpdateTime();
    }

    public int GetTime()
    {
        return time;
    }
}
