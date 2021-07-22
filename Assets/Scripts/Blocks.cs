using UnityEngine;
using UnityEngine.UI;

public class Blocks : MonoBehaviour
{
    public GameObject block; // Префаб блока
    public GameObject leftPillar; // Левая колонка
    public GameObject rightPillar; // Правая колонка

    private int elements; // Количество блоков в одной колонке
    private GameObject[] objBlock; // Массив блоков на сцене
    private bool[] matchBlock; // Массив статусов блока (True = соответствие найдено)
    private int startBlock, endBlock; // Номера выбранных блоков в массиве "objBlock"
    private Color[] colorBlock; // Массив цветов

    private GameController gameController;
    private Line line;
    private Score score;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        line = GetComponent<Line>();
        score = GetComponent<Score>();
    }

    public void CreateBlocks() // Создание блоков на сцене
    {
        objBlock = new GameObject[elements * 2];
        matchBlock = new bool[elements * 2];

        for (int i = 0; i < objBlock.Length; i++)
        {
            objBlock[i] = Instantiate(block);
            objBlock[i].name = "B" + i;

            if (i < objBlock.Length / 2)
            {
                objBlock[i].transform.SetParent(leftPillar.transform, false);
            }
            else
            {
                objBlock[i].transform.SetParent(rightPillar.transform, false);
            }
        }

        FillColors();
        ColoringBlocks();
    }

    private void FillColors() // Рандомное заполнение массива цветами
    {
        colorBlock = new Color[elements];

        for (int i = 0; i < elements; i++)
        {
            colorBlock[i] = new Color(Random.value, Random.value, Random.value, 1);
        }
    }

    private void ColoringBlocks() // Окрашивание блоков
    {
        int numberColor = 0;
        for (int i = 0; i < objBlock.Length; i++)
        {
            int ID = Random.Range(numberColor, elements);
            objBlock[i].GetComponent<Image>().color = colorBlock[ID];

            ArrayShift(numberColor, ID);

            if (numberColor < objBlock.Length / 2 - 1)
            {
                numberColor++;
            }
            else
            {
                numberColor = 0;
            }
        }
    }

    private void ArrayShift(int i, int j) // Смещение использованного цвета
    {
        Color c = colorBlock[i];
        colorBlock[i] = colorBlock[j];
        colorBlock[j] = c;
    }

    public void CheckBlock(int numberBlock, string nameBlock, Vector2 position) // Выбирание блока без пары
    {
        for (int i = 0; i < objBlock.Length; i++)
        {
            if (objBlock[i].name == nameBlock)
            {
                if (!matchBlock[i] && gameController.GetStatusGame())
                {
                    SelectBlock(numberBlock, i);
                    if (numberBlock == 1) line.CreateLine(false, objBlock[i].GetComponent<Image>().color, position, position);
                    if (numberBlock == 2) BlockComparison();
                }
                break;
            }
        }
    }

    private void SelectBlock(int numberBlock, int ID)  // Запоминание выбранных блоков
    {
        if (numberBlock == 1) startBlock = ID;
        if (numberBlock == 2) endBlock = ID;
    }

    private void BlockComparison() // Проверка выбранных блоков на совместимость по цвету
    {
        if (objBlock[startBlock].GetComponent<Image>().color == objBlock[endBlock].GetComponent<Image>().color && gameController.GetStatusGame())
        {
            Vector2 startBlockPosition = Camera.main.ScreenToWorldPoint(objBlock[startBlock].transform.position);
            Vector2 endBlockPosition = Camera.main.ScreenToWorldPoint(objBlock[endBlock].transform.position);
            line.CreateLine(true, objBlock[startBlock].GetComponent<Image>().color, startBlockPosition, endBlockPosition);

            matchBlock[startBlock] = true;
            matchBlock[endBlock] = true;

            score.AddScore();
            CheckingAllBlocks();
        }
    }

    private void CheckingAllBlocks() // Проверка всех блоков на нахождение своей пары
    {
        bool isWin = true;
        for (int i = 0; i < matchBlock.Length; i++)
        {
            if (matchBlock[i] == false)
            {
                isWin = false;
                break;
            }
        }

        if (isWin) gameController.Winner();
    }

    public void SetElements(int e)
    {
        elements = e;
    }
}
