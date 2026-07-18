using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GridTileBase blockPrefab;

    private Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void Start()
    {
        Vector3Int[] coordinates = new Vector3Int[gridSize.x * gridSize.y];

        int index = 0;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                coordinates[index] = new Vector3Int(x, 0, z);

                Vector3 position = _grid.GetCellCenterWorld(coordinates[index]); 
                GridTileBase block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                block.Initialize(position);
            }
        }
    }
}
