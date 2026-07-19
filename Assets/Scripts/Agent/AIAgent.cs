using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple AI that follows the player by moving to the closest free tile
/// adjacent to the player's current position.
/// </summary>
public class AIAgent : GridAgent, IAI
{
    private Vector2Int lastDestination;

    public void Start()
    {
        OnMoveCompleted += OnAgentMove;
    }

    private void OnDestroy()
    {
        OnMoveCompleted -= OnAgentMove;
    }

    /// <summary>
    /// Reacts when the player finishes moving and updates the AI's target.
    /// </summary>
    private void OnAgentMove(UnitType type, Vector2Int coordinate)
    {
        if (type != UnitType.Player)
            return;

        lastDestination = coordinate;
        MoveNear(coordinate);
    }

    /// <summary>
    /// Attempts to move to the nearest available tile adjacent to the target.
    /// </summary>
    public void MoveNear(Vector2Int destination)
    {
        if (IsMoving) return;

        List<Vector2Int> candidates = new List<Vector2Int>();
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int candidate = destination + direction;
            if (gridSettings.IsInBound(candidate) && !obstacleData.IsBlocked(candidate))
                candidates.Add(candidate);
        }

        if (candidates.Contains(_currPoint)) return;

        candidates.Sort((a, b) =>
            ManhattanDistance(_currPoint, a).CompareTo(ManhattanDistance(_currPoint, b)));

        foreach (Vector2Int candidate in candidates)
        {
            if (MoveTo(candidate)) return;
        }
    }

    /// <summary>
    /// Faces the player after completing movement.
    /// </summary>
    protected override void OnMovementFinished()
    {
        Vector2Int direction = lastDestination - _currPoint;

        if (direction == Vector2Int.zero)
            return;

        transform.forward = new Vector3(direction.x, 0f, direction.y);
    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
}