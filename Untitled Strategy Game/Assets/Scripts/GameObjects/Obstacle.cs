using UnityEngine;

public class Obstacle : GameElement
{
    protected override void Init() { }

    void OnMouseDown()
    {
        if (GameManager.GameState == GameState.Game)
        {
            GameStatistics.SetObstacle(this);
        }
        else if (GameManager.GameState == GameState.Editor)
        {
            MapEditorManager.SelectedCharacter = this;
            MapEditorManager.SelectedImage = null;
            EditorStatistics.NameText.text = Name.ToString();
            EditorStatistics.HeathText.text = MaxHealth.ToString();
            EditorStatistics.AttackText.interactable = false;
            EditorStatistics.AttackText.text = "-";
            EditorStatistics.InitiativeText.interactable = false;
            EditorStatistics.InitiativeText.text = "-";
            EditorStatistics.RangeText.interactable = false;
            EditorStatistics.RangeText.text = "-";
            EditorStatistics.MovementText.interactable = false;
            EditorStatistics.MovementText.text = "-";
        }
    }
}
