using UnityEngine;

public class Line : MonoBehaviour
{
    public Material material;

    private LineRenderer line;

    public void CreateLine(bool isAuto, Color colorLine, Vector2 startPosition, Vector2 endPosition) // Создание линии на сцене
    {
        string nameObject = "PlayerLine";
        if (isAuto) nameObject = "AutoLine";

        line = new GameObject(nameObject).AddComponent<LineRenderer>();

        line.tag = "Line";
        line.material = material;
        line.positionCount = 2;
        line.numCapVertices = 50;
        line.startColor = colorLine;
        line.endColor = colorLine;

        float widthLine = 0.3f;
        if (isAuto) widthLine = 0.1f;

        line.startWidth = widthLine;
        line.endWidth = widthLine;

        PositionLine(0, startPosition);
        PositionLine(1, endPosition);

        if (isAuto) line = null;
    }

    public void PositionLine(int point, Vector2 position) // Присвоение координат
    {
        if (line) line.SetPosition(point, position);
    }

    public void DeletePlayerLine() // Удаление линии со сцены
    {
        if (line)
        {
            line = null;
            Destroy(GameObject.Find("PlayerLine"));
        }
    }
}