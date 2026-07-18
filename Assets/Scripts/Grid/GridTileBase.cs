using UnityEngine;

public class GridTileBase : MonoBehaviour
{
    private Vector2Int _position;

    public Vector2Int Position => _position;

    public void Initialize(Vector2Int pos)
    {
        _position = pos;
    }
}