using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None,
    Player,
    Enemy
}

/// <summary>
/// Base class for any unit that moves on the grid using pathfinding.
/// Handles movement, rotation, and grid position tracking.
/// </summary>
public class GridAgent : MonoBehaviour
{
    [Header("Grid")]
    protected Grid grid;
    protected GridSettings gridSettings;
    protected ObstacleData obstacleData;

    [Header("Path Finding")]
    protected PathFinder pathFinder;

    [Header("Settings")]
    [SerializeField] protected UnitType unitType;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float baseOffset = 1f;
    [SerializeField] private float rotationSpeed = 360f;

    /// <summary>
    /// Raised when any GridAgent completes its movement.
    /// </summary>
    public static event Action<UnitType, Vector2Int> OnMoveCompleted;

    /// <summary>
    /// Raised when this agent starts or stops moving.
    /// </summary>
    public event Action<bool> OnMove;

    /// <summary>
    /// True while the agent is following a path.
    /// </summary>
    public bool IsMoving => _isMoving;

    protected Vector2Int _currPoint;
    protected readonly Vector2Int[] _directions = new Vector2Int[]
    {
        Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left,
    };

    private bool _isMoving;

    /// <summary>
    /// Initializes the agent and places it on the nearest valid grid cell.
    /// </summary>
    public void Initialize(Grid grid, GridSettings gridSettings, ObstacleData obstacleData,
        PathFinder pathFinder , Vector2Int startingPoint)
    {
        this.grid = grid;
        this.gridSettings = gridSettings;
        this.obstacleData = obstacleData;
        this.pathFinder = pathFinder;

        if (!pathFinder.IsValid(startingPoint, gridSettings, obstacleData))
        {
            startingPoint = FindNearestValidPoint(startingPoint);
        }

        transform.position = grid.GetCellCenterWorld(
            new Vector3Int(startingPoint.x, 0, startingPoint.y));

        transform.position += new Vector3(0, baseOffset, 0);

        _currPoint = startingPoint;
    }

    /// <summary>
    /// Requests movement to the target grid position.
    /// Returns false if movement cannot begin.
    /// </summary>
    public bool MoveTo(Vector2Int destinationPoint)
    {
        if (_isMoving)
            return false;

        if (pathFinder == null || gridSettings == null || obstacleData == null)
            return false;

        List<Vector2Int> path = pathFinder.FindPath(
            _currPoint, destinationPoint, gridSettings, obstacleData);

        if (path == null || path.Count == 0)
            return false;

        StartCoroutine(MoveToRoutine(path));

        return true;
    }

    /// <summary>
    /// Rotates and moves the agent along the calculated path.
    /// </summary>
    private IEnumerator MoveToRoutine(List<Vector2Int> path)
    {
        _isMoving = true;
        OnMove?.Invoke(_isMoving);

        foreach (Vector2Int step in path)
        {
            Vector2Int direction = step - _currPoint;

            if (direction != Vector2Int.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(
                    new Vector3(direction.x, 0f, direction.y));

                while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime);

                    yield return null;
                }

                transform.rotation = targetRotation;
            }

            Vector3 startPosition = transform.position;
            Vector3 endPosition = grid.GetCellCenterWorld(
                new Vector3Int(step.x, 0, step.y));

            endPosition += new Vector3(0, baseOffset, 0);

            float duration = Vector3.Distance(startPosition, endPosition) / Mathf.Max(moveSpeed, 0.01f);
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                yield return null;
            }

            transform.position = endPosition;
            _currPoint = step;
        }

        OnMoveCompleted?.Invoke(unitType, path[^1]);
        OnMovementFinished();

        _isMoving = false;
        OnMove?.Invoke(_isMoving);
    }

    /// <summary>
    /// Finds the closest walkable tile using a breadth-first search.
    /// </summary>
    private Vector2Int FindNearestValidPoint(Vector2Int origin)
    {
        Vector2Int size = gridSettings.GridSize;
        Vector2Int clamped = new Vector2Int(
            Mathf.Clamp(origin.x, 0, size.x - 1),
            Mathf.Clamp(origin.y, 0, size.y - 1));

        if (pathFinder.IsValid(clamped, gridSettings, obstacleData)) return clamped;

        Queue<Vector2Int> path = new();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>() { clamped };
        path.Enqueue(clamped);

        while (path.Count > 0)
        {
            Vector2Int current = path.Dequeue();

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int next = current + direction;
                if (!gridSettings.IsInBound(next) || visited.Contains(next)) continue;

                if (pathFinder.IsValid(next, gridSettings, obstacleData)) return next;

                visited.Add(next);
                path.Enqueue(next);
            }
        }

        Debug.LogError($"{nameof(GridAgent)}: No open tile found anywhere on the grid.");
        return clamped;
    }

    /// <summary>
    /// Called after the agent finishes moving.
    /// Override to perform custom logic.
    /// </summary>
    protected virtual void OnMovementFinished()
    {
    }
}