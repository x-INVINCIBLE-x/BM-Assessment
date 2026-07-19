using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Breadth-First Search (BFS) pathfinding implementation for grid movement.
/// Finds the shortest path on an unweighted grid.
/// </summary>
[CreateAssetMenu(menuName = "Grid/Path/BFS Path Finder", fileName = "BFS Path Finder")]
public class BFSPathFinder : PathFinder
{
    private Vector2Int[] _directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    /// <summary>
    /// Finds the shortest path between two grid positions using BFS.
    /// </summary>
    public override List<Vector2Int> FindPath(
        Vector2Int start, Vector2Int end,
        GridSettings gridSettings, ObstacleData obstacleData
        )
    {
        if (start == end) return new List<Vector2Int>();
        if (!IsValid(end, gridSettings, obstacleData)) return null;

        Queue<Vector2Int> path = new ();
        Dictionary<Vector2Int, Vector2Int> parent = new ();
        
        path.Enqueue(start);
        parent[start] = start;

        while (path.Count > 0)
        {
            Vector2Int curr = path.Dequeue();

            foreach (Vector2Int dir in _directions)
            {
                Vector2Int next = curr + dir;

                if (IsValid(next, gridSettings, obstacleData) && !parent.ContainsKey(next))
                {
                    path.Enqueue(next);
                    parent[next] = curr;

                    if (next == end)
                        break;
                }
            }
        }

        return parent.ContainsKey(end) ? CreatePath(parent, start, end) : null;
    }

    /// <summary>
    /// Reconstructs the final path from the parent lookup generated during the search.
    /// </summary>
    private List<Vector2Int> CreatePath(Dictionary<Vector2Int, Vector2Int> parent, Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = new();
        Vector2Int curr = end;

        while (curr != start)
        {
            path.Add(curr);
            curr = parent[curr];
        }

        path.Reverse();
        return path;
    }

    /// <summary>
    /// Returns whether a grid position is within bounds and not blocked.
    /// </summary>
    /// <param name="point"> Coordinate to check. </param>
    /// <param name="gridSettings"> Bound Check for Coordinate on specific Grid.</param>
    /// <param name="obstacleData"> Obstacle Check for Coordinate on specific Grid. </param>
    /// <returns> True if block is walkable (Is in Bound and has no Obstacle) </returns>
    public override bool IsValid(Vector2Int point, GridSettings gridSettings, ObstacleData obstacleData)
    {
        if (gridSettings.IsInBound(point) && !obstacleData.IsBlocked(point))
            return true;

        return false;
    }
}
