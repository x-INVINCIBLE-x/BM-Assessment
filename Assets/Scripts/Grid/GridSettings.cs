using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "Grid/Grid Settings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);

    public Vector2Int GridSize => gridSize;
    public int TileCount => gridSize.x * gridSize.y;
}