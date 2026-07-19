using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAgent : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Grid grid;
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private ObstacleData obstacleData;

    [Header("Path Finding")]
    [SerializeField] private PathFinder pathFinder;

    [Header("Settings")]
    [SerializeField] private Vector2Int startingPoint;
    [SerializeField] private float moveSpeed = 5f;

    public bool IsMoving => _isMoving;

    private Vector2Int _currPoint;
    private bool _isMoving;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (!gridSettings.IsInBound(startingPoint))
        {
            startingPoint = new Vector2Int(0, 0);
            Debug.LogWarning("Invalid Starting Point Entered, Reverting to {0, 0}");
        }

        transform.position = grid.GetCellCenterWorld(
            new Vector3Int(startingPoint.x, 0, startingPoint.y));

        _currPoint = startingPoint;
    }

    [ContextMenu("Move")]
    private void MoveTo()
    {
        MoveTo(new Vector2Int(9, 8));
    }

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

    private IEnumerator MoveToRoutine(List<Vector2Int> path)
    {
        _isMoving = true;


        foreach (Vector2Int step in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = grid.GetCellCenterWorld(
                new Vector3Int(step.x, 0, step.y));

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

        _isMoving = false;
    }
}