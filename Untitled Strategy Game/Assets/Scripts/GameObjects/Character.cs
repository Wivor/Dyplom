using System.Collections.Generic;
using UnityEngine;

public class Character : GameElement
{
    public int Queue;

    public List<Action> actions = new List<Action>();

    public int ID;
    public string Owner;
    public int Attack = 10;
    public int Initiative = 10;
    public int Range = 1;
    public int Movement = 2;

    void OnMouseDown()
    {
        if (GameManager.GameState == GameState.Game)
        {
            GameStatistics.SetCharacter(this);
        }
        else if (GameManager.GameState == GameState.Editor)
        {
            MapEditorManager.SelectedCharacter = this;
            MapEditorManager.SelectedImage = null;
            EditorStatistics.NameText.text = Name.ToString();
            EditorStatistics.HeathText.text = MaxHealth.ToString();
            EditorStatistics.AttackText.interactable = true;
            EditorStatistics.AttackText.text = Attack.ToString();
            EditorStatistics.InitiativeText.interactable = true;
            EditorStatistics.InitiativeText.text = Initiative.ToString();
            EditorStatistics.RangeText.interactable = true;
            EditorStatistics.RangeText.text = Range.ToString();
            EditorStatistics.MovementText.interactable = true;
            EditorStatistics.MovementText.text = Movement.ToString();
        }
    }

    protected override void Init()
    {
        Action move = FindObjectOfType<ActionsStorage>().GetActionByName("Move");
        move.Initialize(this);
        actions.Add(move);

        Action attack = FindObjectOfType<ActionsStorage>().GetActionByName("Attack");
        attack.Initialize(this);
        actions.Add(attack);
    }
}
