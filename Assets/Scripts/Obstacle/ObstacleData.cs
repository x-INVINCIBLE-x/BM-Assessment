using UnityEngine;

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

    public bool IsBlocked(int x, int z)
    {
        if (gridSettings == null) return false;
        return blockedTiles[GetIndex(x, z)];
    }

    public void SetBlocked(int x, int z, bool blocked)
    {
        if (gridSettings == null) return;
        EnsureArraySize();
        blockedTiles[GetIndex(x, z)] = blocked;
    }

    public void ToggleBlocked(int x, int z)
    {
        SetBlocked(x, z, !IsBlocked(x, z));
    }

    public void ClearAll()
    {
        for (int i = 0; i < blockedTiles.Length; i++)
        {
            blockedTiles[i] = false;
        }
    }

    private int GetIndex(int x, int z) => x * gridSettings.GridSize.y + z;
}