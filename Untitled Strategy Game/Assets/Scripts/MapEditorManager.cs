using UnityEngine;
using UnityEngine.UI;

public class MapEditorManager : MonoBehaviour
{
    public GameObject SelectedImage;
    public GameElement SelectedCharacter;
    public Dropdown TeamDropdown;
    public InputField width;
    public InputField height;
    public Grid Grid;

    public void GenerateGrid()
    {
        Grid.ClearGrid();
        Grid.GenerateGrid(int.Parse(width.textComponent.text), int.Parse(height.textComponent.text));
    }

    public void ClearGrid()
    {
        Grid.ClearGrid();
    }
}
