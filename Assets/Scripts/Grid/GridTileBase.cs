using UnityEngine;

/// <summary>
/// Class used to hold each tile position data based on grid.
/// It is attatched to each block of a Grid.
/// </summary>
public class GridTileBase : MonoBehaviour
{
    private Vector2Int _position;

    public Vector2Int Position => _position;

    public void Initialize(Vector2Int pos)
    {
        _position = pos;
    }
}