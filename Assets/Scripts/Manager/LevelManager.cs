using UnityEngine;

/// <summary>
/// Handles Player and AI Placement
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Grid grid;
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private ObstacleData obstacleData;

    /// <summary>
    /// Path Finder Algorithm used by Player and AI to get to their destination.
    /// CAn be replaced to any other algorithm from Inspector.
    /// </summary>
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

    /// <summary>
    /// Instantiates and Initializes Player and AI
    /// </summary>
    private void Initialize()
    {
        player = Instantiate(player);
        player.Initialize(grid, gridSettings, obstacleData, pathFinder, playerStarting);

        ai = Instantiate(ai);
        ai.Initialize(grid, gridSettings, obstacleData, pathFinder, aiStarting);
    }
}
