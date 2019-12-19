using System;
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

    public void ConnectNeighbours()
    {
        bool even = position.y % 2 == 0;

        if (!even)
        {
            //AddNeighbour(new Position(position.x, position.y - 1));
            //AddNeighbour(new Position(position.x + 1, position.y - 1));
            //AddNeighbour(new Position(position.x - 1, position.y));
            AddNeighbour(new Position(position.x + 1, position.y));
            AddNeighbour(new Position(position.x, position.y + 1));
            AddNeighbour(new Position(position.x + 1, position.y + 1));
        }
        else
        {
            //AddNeighbour(new Position(position.x - 1, position.y - 1));
            //AddNeighbour(new Position(position.x, position.y - 1));
            //AddNeighbour(new Position(position.x - 1, position.y));
            AddNeighbour(new Position(position.x + 1, position.y));
            AddNeighbour(new Position(position.x - 1, position.y + 1));
            AddNeighbour(new Position(position.x, position.y + 1));
        }
    }

    private void AddNeighbour(Position position)
    {
        if (FindObjectOfType<Storage>().GetHexByPosition(position) != null)
        {
            Node node = FindObjectOfType<Storage>().GetHexByPosition(position).GetComponent<Node>();
            neighbours.Add(node);
            node.neighbours.Add(this);
        }
    }
}
