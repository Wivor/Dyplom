using UnityEngine;

public class Obstacle : GameElement
{
    protected override void Init() { }
    /*
    void OnMouseDown()
    {
        if (GameManager.GameState == GameState.Game)
        {
            GameStatistics.SetObstacle(this);
        }
        else if (GameManager.GameState == GameState.Editor)
        {
            MapEditorManager.SelectedCharacter = null;
            MapEditorManager.SelectedImage = null;
            EditorStatistics.SetObstacle(this);
        }
    }*/
}
