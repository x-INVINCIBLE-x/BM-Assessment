using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "Grid/Grid Settings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);

    public Vector2Int GridSize => gridSize;
    public int TileCount => gridSize.x * gridSize.y;

    public bool IsInBound(Vector2Int point)
    {
        return IsInBound(point.x, point.y);
    }

    public bool IsInBound(int x, int y)
    {
        return x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y;
    }
}