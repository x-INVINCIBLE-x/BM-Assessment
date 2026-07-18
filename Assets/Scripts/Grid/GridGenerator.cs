using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private GridTileBase blockPrefab;

    private Grid _grid;
    GridTileBase[] _gridBlocks; 

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void Start()
    {
        Vector2Int gridSize = gridSettings.GridSize;
        _gridBlocks = new GridTileBase[gridSize.x * gridSize.y];

        int index = 0;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                Vector3 position = _grid.GetCellCenterWorld(new Vector3Int(x, 0, z)); 
                GridTileBase block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                _gridBlocks[index] = block;
                block.Initialize(new Vector2Int(x, z));
            }
        }
    }
}
