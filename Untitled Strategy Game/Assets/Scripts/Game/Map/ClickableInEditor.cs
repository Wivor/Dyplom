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

        mapEditor.selectedCharacter = character;
        mapEditor.selectedImage = null;
        FindObjectOfType<EditorStatistics>().SetCharacter(character);
    }

    private void HexBehaviour()
    {
        Hex hex = GetComponent<Hex>();
        MapEditor mapEditor = FindObjectOfType<MapEditor>();
        if (mapEditor.selectedImage != null && !hex.IsOccupied())
        {
            GameObject obj = Instantiate(mapEditor.selectedImage, transform, true);
            obj.transform.position = transform.position + new Vector3(0, 2, 0);
            if (obj.GetComponent<Character>() != null)
            {
                obj.GetComponent<Character>().statistics.Team = mapEditor.teamDropdown.captionText.text;
                FindObjectOfType<GameManager>().AddNewCharacter(obj.GetComponent<Character>());

                if (mapEditor.teamDropdown.captionText.text == "Team A")
                {
                    obj.GetComponent<Renderer>().material = hex.teamAmat;
                    obj.GetComponent<Character>().teamId = 0;
                }
                else
                {
                    obj.GetComponent<Renderer>().material = hex.teamBmat;
                    obj.GetComponent<Character>().teamId = 1;
                }
            }
            if(obj.GetComponent<Obstacle>() != null)
            {
                Storage.obstacles.Add(obj.transform);
            }
        }
        else if (mapEditor.selectedCharacter != null && !hex.IsOccupied())
        {
            mapEditor.selectedCharacter.transform.SetParent(transform, false);
        }
    }
}
