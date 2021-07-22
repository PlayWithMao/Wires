using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 mousePos; // Позиция курсора мыши

    private Line line;
    private Blocks block;

    private void Awake()
    {
        line = FindObjectOfType<Line>();
        block = FindObjectOfType<Blocks>();
    }

    public void OnPointerDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        block.CheckBlock(1, name, mousePos);
    }
    public void OnDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        line.PositionLine(1, mousePos);
    }

    public void OnDrop() 
    {
        block.CheckBlock(2, name, new Vector2(0, 0));
    }

    public void OnPointerUp()
    {
        line.DeletePlayerLine();
    }
}