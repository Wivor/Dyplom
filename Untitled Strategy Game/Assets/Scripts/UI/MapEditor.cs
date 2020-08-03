using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public GameObject SelectedImage;
    public Character SelectedCharacter;
    public Dropdown TeamDropdown;
    public Grid Grid;

    /*
     * UI InputFields that are intended for grid dimensions.
     */

    public InputField width;
    public InputField height;

    /*
     * Generates map with grid dimensions taken from UI.
     */

    public void GenerateGrid()
    {
        Grid.GenerateGrid(int.Parse(width.textComponent.text), int.Parse(height.textComponent.text));
    }

    /*
     * Clears the map.
     */

    public void ClearGrid()
    {
        Grid.ClearGrid();
    }
}
