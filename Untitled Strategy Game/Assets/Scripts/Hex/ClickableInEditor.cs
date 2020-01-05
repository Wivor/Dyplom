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

    private void CharacterBehaviour()
    {
        Character character = GetComponent<Character>();
        MapEditorManager mapEditorManager = FindObjectOfType<MapEditorManager>();

        mapEditorManager.SelectedCharacter = character;
        mapEditorManager.SelectedImage = null;
        FindObjectOfType<EditorStatistics>().SetCharacter(character);
    }

    private void HexBehaviour()
    {
        Hex hex = GetComponent<Hex>();
        MapEditorManager mapEditorManager = FindObjectOfType<MapEditorManager>();
        if (mapEditorManager.SelectedImage != null && !hex.IsOccupied())
        {
            GameObject obj = Instantiate(mapEditorManager.SelectedImage);
            obj.transform.position = transform.position + new Vector3(0, 2, 0);
            obj.transform.parent = transform;
            if (obj.GetComponent<Character>() != null)
            {
                obj.GetComponent<Character>().Statistics.Team = mapEditorManager.TeamDropdown.captionText.text;
                FindObjectOfType<GameManager>().AddNewCharacter(obj.GetComponent<Character>());

                if (mapEditorManager.TeamDropdown.captionText.text == "Team A")
                    obj.GetComponent<Renderer>().material = hex.TeamAmat;
                else
                    obj.GetComponent<Renderer>().material = hex.TeamBmat;

            }
        }
        else if (mapEditorManager.SelectedCharacter != null && !hex.IsOccupied())
        {
            mapEditorManager.SelectedCharacter.transform.SetParent(transform, false);
        }
    }
}
