using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Grid grid;
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private ObstacleData obstacleData;

    [Header("Path Finding")]
    [SerializeField] private PathFinder pathFinder;

    [Header("Player")]
    [SerializeField] private PlayerAgent player;
    [SerializeField] private Vector2Int playerStarting;

    [Header("AI")]
    [SerializeField] private AIAgent ai;
    [SerializeField] private Vector2Int aiStarting;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        player = Instantiate(player);
        player.Initialize(grid, gridSettings, obstacleData, pathFinder, playerStarting);

        ai = Instantiate(ai);
        ai.Initialize(grid, gridSettings, obstacleData, pathFinder, aiStarting);
    }
}
