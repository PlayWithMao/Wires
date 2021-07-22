using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TableHighscore : MonoBehaviour
{
    public GameObject panelLiders; // Окно "Таблица рекордов"
    public GameObject container; // Поле с рекордами
    public GameObject highscorePlayer; // Префаб строки для отображения рекорда

    private List<Highscore> HighscoreList; // Массив со значениями имен и рекордов
    private List<Transform> HighscoreTransformList; // Массив объектов (префабов) с информацией
    void Awake()
    {
        HighscoreList = new List<Highscore>();
    }

    public void AddHighscore(int score, string name) // Добавление нового рекорда
    {
        Highscore highscore = new Highscore { score = score, name = name };
        HighscoreList.Add(highscore);

        UpdateTable();
    }

    private void UpdateTable() // Обновление таблицы рекордов
    {
        SortTable();

        if (HighscoreList.Count > 10) DeleteHighscore();

        HighscoreTransformList = new List<Transform>();
        foreach (Highscore highscore in HighscoreList)
        {
            CreateStringInTable(highscore, container, HighscoreTransformList);
        }
    }

    private void SortTable() // Сортировка таблицы по убыванию
    {
        for (int i = 0; i < HighscoreList.Count; i++)
        {
            for (int j = i + 1; j < HighscoreList.Count; j++)
            {
                if (HighscoreList[j].score > HighscoreList[i].score)
                {
                    Highscore tmp = HighscoreList[i];
                    HighscoreList[i] = HighscoreList[j];
                    HighscoreList[j] = tmp;
                }
            }
        }
    }

    private void DeleteHighscore() // Удаление рекорда
    {
        HighscoreList.RemoveAt(10);
    }

    private void CreateStringInTable(Highscore highscore, GameObject container, List<Transform> transformList) // Создание строки на сцене
    {
        float distance = 35f;
        GameObject obj = Instantiate(highscorePlayer, container.transform);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -distance * transformList.Count);

        obj.SetActive(true);

        int number = transformList.Count + 1;

        Text[] objText = obj.GetComponentsInChildren<Text>();
        objText[0].text = number.ToString();
        objText[1].text = highscore.name;
        objText[2].text = highscore.score.ToString();

        transformList.Add(obj.transform);
    }

    public void OnClickStart() // Рестарт игры
    {
        ClearTable();

        panelLiders.SetActive(false);
        FindObjectOfType<GameController>().RestartLevel();
    }

    private void ClearTable() // Почистить таблицу
    {
        for (int i = 0; i < HighscoreTransformList.Count; i++)
        {
            Destroy(HighscoreTransformList[i].gameObject);
        }
    }

    public class Highscore
    {
        public int score;
        public string name;
    }
}
