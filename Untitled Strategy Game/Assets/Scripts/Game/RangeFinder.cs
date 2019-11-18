using System.Collections.Generic;
using UnityEngine;

public class RangeFinder
{
    public List<Node> inRange = new List<Node>();

    public void FindNodesInRange(Node node, int range, int iteration = 1)
    {
        Debug.Log(range);
        if (range != 0)
        {
            foreach (Node neighbour in node.neighbours)
            {
                if(neighbour.distance == 0 || neighbour.distance < iteration)
                {
                    neighbour.distance = iteration;
                    inRange.Add(neighbour);
                    FindNodesInRange(neighbour, range-1, iteration++);
                }
            }
        }
    }
}
