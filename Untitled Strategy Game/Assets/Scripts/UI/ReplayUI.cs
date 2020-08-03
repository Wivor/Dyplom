using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUI : MonoBehaviour
{
    public Toggle replayToggle;
    public InputField nameField;
    public Dropdown nameDropdown;
    
    private ReplayManager _replayManager;

    private void Start()
    {
        _replayManager = FindObjectOfType<ReplayManager>();
        string[] fileArray = Directory.GetFiles("replays/").Select(Path.GetFileName).ToArray();
        nameDropdown.AddOptions(fileArray.ToList());
    }

    public void OnGameStart()
    {
        if (replayToggle.isOn)
        {
            _replayManager.CreateReplayData(nameField.text, FindObjectOfType<SaveMapManager>().GetSaveData());
        }
        else
        {
            _replayManager.ClearReplay();
        }
    }

    public void OnReplayStart()
    {
        _replayManager.LoadReplay(nameDropdown.captionText.text);
        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        EventManager.ReplayStartTrigger();
    }
}
