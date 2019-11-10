using UnityEngine;

public class HexEditor : MonoBehaviour
{
    public Material TeamAmat;
    public Material TeamBmat;

    void OnMouseDown()
    {
        if (enabled == true)
        {
            if (FindObjectOfType<MapEditorManager>().SelectedImage != null && !isOccupied())
            {
                GameObject obj = Instantiate(FindObjectOfType<MapEditorManager>().SelectedImage);
                obj.transform.position = transform.position + new Vector3(0, 2, 0);
                obj.transform.parent = transform;
                if (obj.GetComponent<Character>() != null)
                {
                    obj.GetComponent<Character>().Owner = FindObjectOfType<MapEditorManager>().TeamDropdown.captionText.text;
                    FindObjectOfType<GameManager>().AddNewCharacter(obj.GetComponent<Character>());

                    if (FindObjectOfType<MapEditorManager>().TeamDropdown.captionText.text == "Team A")
                        obj.GetComponent<Renderer>().material = TeamAmat;
                    else
                        obj.GetComponent<Renderer>().material = TeamBmat;

                }
            }
            else if (FindObjectOfType<MapEditorManager>().SelectedCharacter != null && !isOccupied())
            {
                FindObjectOfType<MapEditorManager>().SelectedCharacter.transform.SetParent(transform, false);
            }
        }
    }

    public bool isOccupied()
    {
        if (transform.childCount == 0)
            return false;
        return true;
    }

    void OnEnable()
    {
        EventManager.OnGameStart += Disable;
    }

    void OnDisable()
    {
        EventManager.OnGameStart -= Disable;
    }

    public void Disable()
    {
        enabled = false;
    }
}
