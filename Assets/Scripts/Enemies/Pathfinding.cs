using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public class Pathfinding : MonoBehaviour
{
    private struct Node
    {
        public int x;
        public int y;
        
        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool bIsWalkable;
        public int IndexOfPreviousNode;

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
    
    
    private const int STRAIGHT_MOVE_COST = 10;
    private const int DIAGONAL_MOVE_COST = 14;
    private const int cellSize = 1;
    int2 gridSize = new int2(25, 25);
    
    private Vector3 cameraPos;
    
    private void Start()
    {
        
    }

    public List<Vector2> FindPath(Vector2 sPosition, Vector2 ePosition)
    {
        int2 startPosition = worldToGridPosition(sPosition);
        int2 endPosition = worldToGridPosition(ePosition);

        NativeArray<Node> nodeArray = new NativeArray<Node>(gridSize.x * gridSize.y, Allocator.Temp);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.x; y++)
            {
                Node node = new Node();
                node.x = x;
                node.y = y;
                node.index = CalculateIndex(x, y);

                node.gCost = int.MaxValue;
                node.hCost = CalculateDistanceCost(new int2(x, y), endPosition);
                node.CalculateFCost();

                node.bIsWalkable = IsWalkable(new int2(x, y));
                node.IndexOfPreviousNode = -1;

                nodeArray[node.index] = node;
            }
        }

        NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(8, Allocator.Temp);
        neighbourOffsetArray[0] = new int2(-1, 0); //Left
        neighbourOffsetArray[1] = new int2(1, 0); //Right
        neighbourOffsetArray[2] = new int2(0, 1); //Up
        neighbourOffsetArray[3] = new int2(0, -1); //Down
        neighbourOffsetArray[4] = new int2(-1, -1); //Left down
        neighbourOffsetArray[5] = new int2(-1, 1); //Left Up
        neighbourOffsetArray[6] = new int2(1, -1); //Right down
        neighbourOffsetArray[7] = new int2(1, 1); //Right up
        
        int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y);

        Node startNode = nodeArray[CalculateIndex(startPosition.x, startPosition.y)];
        startNode.gCost = 0;
        startNode.CalculateFCost();
        nodeArray[CalculateIndex(startPosition.x, startPosition.y)] = startNode;

        NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        openList.Add(startNode.index);

        while (openList.Length > 0)
        {
            int currentNodeIndex = GetLowestCostFNodeIndex(openList, nodeArray);
            Node currentNode = nodeArray[currentNodeIndex];

            if (currentNodeIndex == endNodeIndex)
            {
                break;
            }

            for (int i = 0; i < openList.Length; i++)
            {
                if (openList[i] == currentNodeIndex)
                {
                    openList.RemoveAtSwapBack(i);
                    break;
                }
            }
            
            closedList.Add(currentNodeIndex);

            for (int i = 0; i < neighbourOffsetArray.Length; i++)
            {
                int2 neighbourOffset = neighbourOffsetArray[i];
                int2 neighbourPosition = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);
                if (!IsPositionInGrid(neighbourPosition))
                {
                    continue;
                }

                int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y);

                if (closedList.Contains(neighbourNodeIndex))
                {
                    continue;
                }

                Node neighbourNode = nodeArray[neighbourNodeIndex];
                if (!neighbourNode.bIsWalkable)
                {
                    continue;
                }

                int2 currentNodePosition = new int2(currentNode.x, currentNode.y);

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbourPosition);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.IndexOfPreviousNode = currentNodeIndex;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.CalculateFCost();

                    nodeArray[neighbourNodeIndex] = neighbourNode;

                    if (!openList.Contains(neighbourNode.index))
                    {
                        openList.Add(neighbourNode.index);
                    }
                }
            }
        }

        Node endNode = nodeArray[endNodeIndex];

        if (endNode.IndexOfPreviousNode != -1)
        {
            NativeList<int2> path = CalculatePath(nodeArray, endNode);
            List<Vector2> result = new List<Vector2>();
            
            for (int i = path.Length - 1; i >= 0; i--)
            {
                result.Add(gridToWorldPosition(path[i]));
            }
            path.Dispose();
            return result;
        }

        nodeArray.Dispose();
        neighbourOffsetArray.Dispose();
        openList.Dispose();
        closedList.Dispose();

        return new List<Vector2>();
    }

    private int2 worldToGridPosition(Vector3 position)
    {
        return new int2(Mathf.FloorToInt((position.x - transform.position.x) / cellSize + gridSize.x / 2),
        Mathf.FloorToInt((position.y  - transform.position.y) / cellSize + gridSize.y / 2));
    }

    private Vector2 gridToWorldPosition(int2 position)
    {
        return new Vector2(Mathf.Round((position.x - gridSize.x / 2) * cellSize + transform.position.x), Mathf.Round((position.y - gridSize.y / 2) * cellSize + transform.position.y));
    }
    
    private NativeList<int2> CalculatePath(NativeArray<Node> nodeArray, Node endNode)
    {
        if (endNode.IndexOfPreviousNode == -1)
        {
            return new NativeList<int2>(Allocator.Temp);
        }
        else
        {
            NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
            path.Add(new int2(endNode.x, endNode.y));

            Node currentNode = endNode;
            while (currentNode.IndexOfPreviousNode != -1)
            {
                Node previousNode = nodeArray[currentNode.IndexOfPreviousNode];
                path.Add(new int2(previousNode.x, previousNode.y));
                currentNode = previousNode;
            }

            return path;
        }
    }

    private int CalculateIndex(int x, int y)
    {
        return x + y * gridSize.x;
    }

    private int CalculateDistanceCost(int2 position, int2 targetPosition)
    {
        int xDistance = math.abs(position.x - targetPosition.x);
        int yDistance = math.abs(position.y - targetPosition.y);
        int remaining = math.abs(xDistance - yDistance);
        return DIAGONAL_MOVE_COST * math.min(xDistance, yDistance) + STRAIGHT_MOVE_COST * remaining;
    }

    private bool IsWalkable(int2 position)
    {
        Vector2 newPosition = gridToWorldPosition(position);
        
        Vector3 start = new Vector3(newPosition.x - cellSize, newPosition.y - cellSize);
        Vector3 end = new Vector3(newPosition.x + cellSize, newPosition.y + cellSize);
        RaycastHit2D hit = Physics2D.Raycast(start, end - start, Vector2.Distance(start, end));
        
        if (hit && (hit.collider.CompareTag("Destructible") || hit.collider.CompareTag("Doors") || hit.collider.CompareTag("Environment")
            || hit.collider.CompareTag("Obstacles") || hit.collider.CompareTag("Trap")))
        {
            return false;
        }

        return true;
    }
    
    private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<Node> nodeArray)
    {
        Node lowestCostNode = nodeArray[openList[0]];
        for (int i = 1; i < openList.Length; i++)
        {
            Node helperNode = nodeArray[openList[i]];
            if (helperNode.fCost < lowestCostNode.fCost)
            {
                lowestCostNode = helperNode;
            }
        }

        return lowestCostNode.index;
    }

    private bool IsPositionInGrid(int2 gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridSize.x && gridPosition.y < gridSize.y;
    }
}
