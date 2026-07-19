using System.Collections.Generic;
using UnityEngine;

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

    private void OnAgentMove(UnitType type, Vector2Int coordinate)
    {
        if (type != UnitType.Player)
            return;

        lastDestination = coordinate;
        MoveNear(coordinate);
    }

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

    protected override void OnMovementFinished()
    {
        Vector2Int direction = lastDestination - _currPoint;

        if (direction == Vector2Int.zero)
            return;

        transform.forward = new Vector3(direction.x, 0f, direction.y);
    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
}