using System.Collections.Generic;
using UnityEngine;

public class Character : GameElement
{
    public int Queue;

    public List<Action> actions = new List<Action>();

    public int id;
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
        Action move = FindObjectOfType<Storage>().GetActionByName("Move");
        actions.Add(move);

        Action attack = FindObjectOfType<Storage>().GetActionByName("Attack");
        actions.Add(attack);
    }

    public void InitializeActions()
    {
        foreach (Action action in actions)
        {
            action.Initialize(this);
        }
    }

    public Action getActionByID(int id)
    {
        return actions.Find(action => action.id == id);
    }

    public void DealDamage(int dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameManager>().DestroyCharacter(this);
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        EventManager.OnGameStart += ChangeGameState;
    }

    public void ChangeGameState()
    {
        gameObject.AddComponent<ClickableInGame>();
    }
}
