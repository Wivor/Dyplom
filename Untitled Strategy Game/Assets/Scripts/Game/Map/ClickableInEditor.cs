using UnityEngine;

public class ClickableInEditor : MonoBehaviour
{
    void OnMouseDown()
    {
        if(GetComponent<Hex>() != null)
        {
            HexBehaviour();
        }
        else if (GetComponent<Character>() != null)
        {
            CharacterBehaviour();
        }
    }

    /*
     * #TODO move all this to MapEditorManager, think about new way of defining game state
     */

    private void CharacterBehaviour()
    {
        Character character = GetComponent<Character>();
        MapEditor mapEditor = FindObjectOfType<MapEditor>();

        mapEditor.SelectedCharacter = character;
        mapEditor.SelectedImage = null;
        FindObjectOfType<EditorStatistics>().SetCharacter(character);
    }

    private void HexBehaviour()
    {
        Hex hex = GetComponent<Hex>();
        MapEditor mapEditor = FindObjectOfType<MapEditor>();
        if (mapEditor.SelectedImage != null && !hex.IsOccupied())
        {
            GameObject obj = Instantiate(mapEditor.SelectedImage);
            obj.transform.position = transform.position + new Vector3(0, 2, 0);
            obj.transform.parent = transform;
            if (obj.GetComponent<Character>() != null)
            {
                obj.GetComponent<Character>().Statistics.Team = mapEditor.TeamDropdown.captionText.text;
                FindObjectOfType<GameManager>().AddNewCharacter(obj.GetComponent<Character>());

                if (mapEditor.TeamDropdown.captionText.text == "Team A")
                {
                    obj.GetComponent<Renderer>().material = hex.TeamAmat;
                    obj.GetComponent<Character>().TeamID = 0;
                }
                else
                {
                    obj.GetComponent<Renderer>().material = hex.TeamBmat;
                    obj.GetComponent<Character>().TeamID = 1;
                }
            }
            if(obj.GetComponent<Obstacle>() != null)
            {
                Storage.obstacles.Add(obj.transform);
            }
        }
        else if (mapEditor.SelectedCharacter != null && !hex.IsOccupied())
        {
            mapEditor.SelectedCharacter.transform.SetParent(transform, false);
        }
    }
}
