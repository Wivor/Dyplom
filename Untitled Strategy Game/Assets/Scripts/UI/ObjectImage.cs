using UnityEngine;

public class ObjectImage : MonoBehaviour
{
    public MapEditorManager Manager;
    public GameObject obj;

    public void ChangeSelection()
    {
        Manager.SelectedImage = obj;
        Manager.SelectedCharacter = null;
    }
}
