using UnityEngine;

/// <summary>
/// Stores the blocked tiles for a grid as a ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "ObstacleData", menuName = "Grid/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private bool[] blockedTiles;

    public GridSettings GridSettings => gridSettings;
    public Vector2Int GridSize => gridSettings != null ? gridSettings.GridSize : Vector2Int.zero;
    public bool[] BlockedTiles => blockedTiles;

    private void OnEnable() => EnsureArraySize();
    private void OnValidate() => EnsureArraySize();

    /// <summary>
    /// Ensures the obstacle array matches the current grid dimensions.
    /// </summary>
    public void EnsureArraySize()
    {
        int requiredLength = gridSettings != null ? gridSettings.TileCount : 0;

        if (gridSettings != null && blockedTiles.Length == requiredLength) return;

        bool[] newArray = new bool[requiredLength];
        if (blockedTiles != null)
        {
            int copyLength = Mathf.Min(blockedTiles.Length, newArray.Length);
            System.Array.Copy(blockedTiles, newArray, copyLength);
        }
        blockedTiles = newArray;
    }

    /// <summary>
    /// Returns whether the specified grid coordinates are blocked.
    /// </summary>
    public bool IsBlocked(Vector2Int point)
    {
        return IsBlocked(point.x, point.y);
    }

    /// <summary>
    /// Returns whether the specified grid coordinates are blocked.
    /// </summary>
    public bool IsBlocked(int x, int z)
    {
        if (gridSettings == null) return false;
        return blockedTiles[GetIndex(x, z)];
    }

    /// <summary>
    /// Marks a tile as blocked or walkable.
    /// </summary>
    public void SetBlocked(int x, int z, bool blocked)
    {
        if (gridSettings == null) return;
        EnsureArraySize();
        blockedTiles[GetIndex(x, z)] = blocked;
    }

    /// <summary>
    /// Toggles the blocked state of a tile.
    /// </summary>
    public void ToggleBlocked(int x, int z)
    {
        SetBlocked(x, z, !IsBlocked(x, z));
    }

    /// <summary>
    /// Sets all tile as walkable.
    /// </summary>
    public void ClearAll()
    {
        for (int i = 0; i < blockedTiles.Length; i++)
        {
            blockedTiles[i] = false;
        }
    }

    private int GetIndex(int x, int z) => x * gridSettings.GridSize.y + z;
}