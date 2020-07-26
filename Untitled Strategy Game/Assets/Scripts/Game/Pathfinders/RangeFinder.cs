using System.Collections.Generic;
using UnityEngine;

public class RangeFinder
{
    public static HashSet<Node> inRange;

    static int range;

    public static HashSet<Node> FindNodesInRange(Node sourceNode, int _range)
    {
        range = _range;
        sourceNode.distance = 1;
        inRange = new HashSet<Node>();
        FindForRing(new HashSet<Node>(sourceNode.neighbours), 1);
        sourceNode.distance = 0;

        return inRange;
    }

    private static void FindForRing(HashSet<Node> nodeRing, int iteration)
    {
        if (range >= iteration)
        {
            SetDistance(nodeRing, iteration);
            inRange.UnionWith(nodeRing);
            HashSet<Node> nextRing = new HashSet<Node>();
            foreach (Node node in nodeRing)
            {
                foreach (Node neighbour in node.neighbours)
                {
                    if(neighbour.distance == 0)
                        nextRing.Add(neighbour);
                }
            }
            FindForRing(nextRing, iteration+1);
        }
    }

    private static void SetDistance(HashSet<Node> nodeRing, int iteration)
    {
        foreach (Node node in nodeRing)
        {
            node.distance = iteration;
        }
    }
}
