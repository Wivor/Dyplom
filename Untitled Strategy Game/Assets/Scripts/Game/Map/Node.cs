using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class added to hexes, used in pathfinding.
 */

public class Node : MonoBehaviour
{
    public Position position;

    /*
     * Distance from node used when calculating distance for pathfinding. 0 stands for current node and nodes out of range.
     */
    public int distance;

    /*
     * List of neighbouring nodes.
     */
    public List<Node> neighbours;

    /*
     * Checks if there are any objects on the hex.
     * 
     * @return      true if hex is empty
     */

    public bool IsPassable()
    {
        return !GetComponent<Hex>().IsOccupied();
    }

    /*
     * Class for saving unique hex position in grid.
     */

    [Serializable]
    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /*
         * Checks if other positions is equal with this one.
         * 
         * @position    Position class to compare with this one
         * 
         * @return      true if are equal
         */

        public bool Equals(Position position)
        {
            return (x == position.x) && (y == position.y);
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    }

    /*
     * Used to find neighbours of this node.
     * Made based on this: https://www.redblobgames.com/grids/hexagons/#neighbors-doubled
     * 
     * Half is commented because current node in also added as a neighbour in the other node so the operation would be repeated.
     */

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

    /*
     * Finds in Storage node with given position and adds it to neighbours list.
     * Current node is also added to neighbours of found node.
     */

    private void AddNeighbour(Position position)
    {
        if (Storage.GetHexByPosition(position) != null)
        {
            Node node = Storage.GetHexByPosition(position).GetComponent<Node>();
            neighbours.Add(node);
            node.neighbours.Add(this);
        }
    }
}
