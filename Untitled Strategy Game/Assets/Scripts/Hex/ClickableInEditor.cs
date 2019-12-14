using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableInEditor : MonoBehaviour
{
    Hex hex;
    MapEditorManager mapEditorManager;

    private void Start()
    {
        hex = GetComponent<Hex>();
    }

    void OnMouseDown()
    {
        mapEditorManager = FindObjectOfType<MapEditorManager>();
        if (mapEditorManager.SelectedImage != null && !hex.IsOccupied())
        {
            GameObject obj = Instantiate(mapEditorManager.SelectedImage);
            obj.transform.position = transform.position + new Vector3(0, 2, 0);
            obj.transform.parent = transform;
            if (obj.GetComponent<Character>() != null)
            {
                obj.GetComponent<Character>().Owner = mapEditorManager.TeamDropdown.captionText.text;
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
