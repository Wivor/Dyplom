using UnityEngine;
using UnityEngine.Serialization;

public class ObjectImage : MonoBehaviour
{
    public MapEditor mapEditor;
    public GameObject obj;

    public void ChangeSelection()
    {
        mapEditor.SelectedImage = obj;
        mapEditor.SelectedCharacter = null;
    }
}
