using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    public SaveMapManager saveMapManager;
    public Text saveName;
    public Dropdown dropdown;

    private void Start()
    {
        DirectoryInfo d = new DirectoryInfo("saves/");
        FileInfo[] files = d.GetFiles("*");
        dropdown.AddOptions(files.Select(file => file.Name).ToList());
    }

    public void SaveMap()
    {
        saveMapManager.SaveMap(saveName.text);
    }

    public void LoadMap()
    {
        saveMapManager.LoadMap(dropdown.captionText.text);
    }
}
