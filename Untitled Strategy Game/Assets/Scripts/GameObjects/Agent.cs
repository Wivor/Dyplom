using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private float[][] policy = new float[1][];
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetType(1);
    }

    public void SetType(int type = 0)
    {
        switch (type)
        {
            case 0:

                break;
            case 1:
                policy[0] = new float[] { 1, 1, 1, 1, 1, 1, 1 };
                break;
            case 2:
                policy[0] = new float[] { 0, 0, 0, 0, 0, 0, 1 };
                break;
        }
    }

    public void TakeAction()
    {
        float max = policy[0].Max();
        float index;
        int count = 0;

        for (int i = 0; i < policy[0].Length; i++)
        {
            if (policy[0][i] == max)
            {
                count++;
            }
        }

        if (count == 1)
        {
            index = policy[0].ToList().IndexOf(max);
        }
        else
        {
            index = Random.Range(0, policy[0].Length);
        }

        Debug.Log(index);

        Node.Position position = Storage.characters[gameManager.Queue].transform.parent.GetComponent<Node>().position;
        bool even = position.y % 2 == 0;

        switch (index)
        {
            case 0:
                if (!even)
                    Move(new Node.Position(position.x, position.y - 1));
                else
                    Move(new Node.Position(position.x - 1, position.y - 1));
                break;
            case 1:
                if (!even)
                    Move(new Node.Position(position.x + 1, position.y - 1));
                else
                    Move(new Node.Position(position.x, position.y - 1));
                break;
            case 2:
                Move(new Node.Position(position.x - 1, position.y));
                break;
            case 3:
                Move(new Node.Position(position.x + 1, position.y));
                break;
            case 4:
                if (!even)
                    Move(new Node.Position(position.x, position.y + 1));
                else
                    Move(new Node.Position(position.x - 1, position.y + 1));
                break;
            case 5:
                if (!even)
                    Move(new Node.Position(position.x + 1, position.y + 1));
                else
                    Move(new Node.Position(position.x, position.y + 1));
                break;
            case 6:
                gameManager.TriggerAction(Storage.characters[gameManager.Queue].id, 2, Storage.GetHexByPosition(position).id);
                break;
        }
    }

    private void Move(Node.Position position)
    {
        if (Storage.GetHexByPosition(position) != null)
            gameManager.TriggerAction(Storage.characters[gameManager.Queue].id, 0, Storage.GetHexByPosition(position).id);
        else
            TakeAction();
    }
}