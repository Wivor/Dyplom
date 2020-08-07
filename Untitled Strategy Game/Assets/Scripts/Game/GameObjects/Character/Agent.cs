using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private float[][] _policy = new float[1][];
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        SetType(1);
    }

    /*
     * Sets character policy based on its team.
     */

    public void SetType(int type = 0)
    {
        switch (type)
        {
            case 0:

                break;
            case 1:
                _policy[0] = new float[] { 1, 1, 1, 1, 1, 1, 1 };
                break;
            case 2:
                _policy[0] = new float[] { 0, 0, 0, 0, 0, 0, 1 };
                break;
        }
    }

    /*
     * Takes the biggest value from policy table and checks if there is only one such number.
     * If there is only one biggest number saves index of this number and takes action associated with that index.
     * If there is more than one takes random action from all available.
     * 
     * #TODO in case of repititon of the biggest number it should only random from indexes of repeaters.
     */

    public void TakeAction()
    {
        float max = _policy[0].Max();
        float index;
        int count = 0;

        for (int i = 0; i < _policy[0].Length; i++)
        {
            if (_policy[0][i] == max)
            {
                count++;
            }
        }

        if (count == 1)
        {
            index = _policy[0].ToList().IndexOf(max);
        }
        else
        {
            index = Random.Range(0, _policy[0].Length);
        }

        //Debug.Log(index);

        Node.Position position = Storage.characters[_gameManager.queue].transform.parent.GetComponent<Node>().position;
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
                _gameManager.TriggerAction(Storage.characters[_gameManager.queue].id, 2, Storage.GetHexByPosition(position).id);
                break;
        }
    }

    /*
     * Checks if hex with given position exist and moves character on it if true.
     * In other case choses to make different action.
     * 
     * @position position of hex to move on.
     */

    private void Move(Node.Position position)
    {
        if (Storage.GetHexByPosition(position) != null)
            _gameManager.TriggerAction(Storage.characters[_gameManager.queue].id, 0, Storage.GetHexByPosition(position).id);
        else
            TakeAction();
    }
}