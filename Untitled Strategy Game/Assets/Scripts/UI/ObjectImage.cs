using UnityEngine;
using UnityEngine.Serialization;

public class ObjectImage : MonoBehaviour
{
    public MapEditor mapEditor;
    public GameObject obj;

    public void ChangeSelection()
    {
        mapEditor.selectedImage = obj;
        mapEditor.selectedCharacter = null;
    }
}
