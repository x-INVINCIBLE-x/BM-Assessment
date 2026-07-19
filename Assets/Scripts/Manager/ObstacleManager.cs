using UnityEngine;

/// <summary>
/// Spawns obstacles based on the assigned ObstacleData asset.
/// </summary>
public class ObstacleManager : MonoBehaviour
{
    /// <summary>
    /// Grid obstacle layout used for spawning.
    /// </summary>
    [SerializeField] private ObstacleData obstacleData;

    /// <summary>
    /// Prefab instantiated for each blocked tile.
    /// </summary>
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Grid grid;

    [Tooltip("Hight offset from Grid's position")]
    [SerializeField] private float heightOffset;
    
    private void Start()
    {
        GenerateObstacles();
    }

    /// <summary>
    /// Instantiates obstacles on every blocked grid cell.
    /// </summary>
    private void GenerateObstacles()
    {
        if (obstacleData == null)
        {
            Debug.LogWarning("No Obstacle Data Assigned");
            return;
        }

        Vector2Int gridSize = obstacleData.GridSize;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                if (!obstacleData.IsBlocked(x, z))
                    continue;

                Vector3 position = grid.GetCellCenterWorld(new Vector3Int(x, 0, z))
                    + new Vector3(0, heightOffset, 0);

                Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
