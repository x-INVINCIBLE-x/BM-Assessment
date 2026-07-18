using UnityEngine;

public class GridTileBase : MonoBehaviour
{
    private Vector2 position;

    public Vector2 Position => position;

    public void Initialize(Vector2 pos)
    {
        position = pos;
    }
}
