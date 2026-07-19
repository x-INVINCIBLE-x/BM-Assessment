using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for grid pathfinding algorithms.
/// As an stateless ScriptableObject same instance can be used for multiple agents.
/// </summary>
public abstract class PathFinder : ScriptableObject
{
    /// <summary>
    /// Calculates a path between two grid positions.
    /// Returns null or an empty list if no valid path exists.
    /// </summary>
    public abstract List<Vector2Int> FindPath(
        Vector2Int start, Vector2Int end,
        GridSettings gridSettings, ObstacleData obstacleData
        );

    /// <summary>
    /// Returns whether the specified grid position can be traversed.
    /// </summary>
    public abstract bool IsValid(Vector2Int point, GridSettings gridSettings, ObstacleData obstacleData);
}