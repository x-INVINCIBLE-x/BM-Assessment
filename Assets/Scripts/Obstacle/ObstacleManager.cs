using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Grid grid;

    [SerializeField] private float heightOffset;
    
    private void Start()
    {
        GenerateObstacles();
    }

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
