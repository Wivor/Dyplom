using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public GameObject selectedImage;
    public Character selectedCharacter;
    public Dropdown teamDropdown;
    public Grid grid;

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
        grid.GenerateGrid(int.Parse(width.textComponent.text), int.Parse(height.textComponent.text));
    }

    /*
     * Clears the map.
     */

    public void ClearGrid()
    {
        grid.ClearGrid();
    }
}
