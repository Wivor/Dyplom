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
        FileInfo[] Files = d.GetFiles("*");
        dropdown.AddOptions(Files.Select(file => { return file.Name; }).ToList());
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
