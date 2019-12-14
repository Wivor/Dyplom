using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Position position;
    public int distance;

    public List<Node> neighbours;

    public bool IsPassable()
    {
        return !GetComponent<Hex>().IsOccupied();
    }

    [System.Serializable]
    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Position position)
        {
            return (x == position.x) && (y == position.y);
        }
    }
}
