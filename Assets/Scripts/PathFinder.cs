using System.Collections.Generic;
using UnityEngine;

public abstract class PathFinder : ScriptableObject
{
    public abstract List<Vector2Int> FindPath(
        Vector2Int start, Vector2Int end,
        GridSettings gridSettings, ObstacleData obstacleData
        );
}