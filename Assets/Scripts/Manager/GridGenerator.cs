using UnityEngine;

/// <summary>
/// Responsible for creating grid and placing tiles using Unity's Grid component
/// </summary>
[RequireComponent(typeof(Grid))]
public class GridGenerator : MonoBehaviour
{
    /// <summary>
    /// Provides the dimension of grid to generate.
    /// </summary>
    [SerializeField] private GridSettings gridSettings;

    /// <summary>
    /// Block prefab used to create the Grid.
    /// </summary>
    [SerializeField] private GridTileBase blockPrefab;

    private Grid _grid;
    GridTileBase[] _gridBlocks; 

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    /// <summary>
    /// Generates Grid by Instantiating blockPrefab based on gridSettings.
    /// Unity' Grid component is used to help determine each block world position based on their index.
    /// </summary>
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
