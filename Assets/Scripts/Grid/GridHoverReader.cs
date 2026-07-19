using UnityEngine;
using TMPro;

/// <summary>
/// Displays the hovered grid position and applies a visual highlight to the tile.
/// </summary>
public class GridHoverReader : MonoBehaviour
{
    [SerializeField] private GameObject gridPosUI;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private Vector3 offset;

    [Header("Hover Effect")]
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private float hoverScale = 1.15f;

    private Renderer _currentRenderer;
    private Color _originalColor;
    private Vector3 _originalScale;

    private GridTileBase _currentTile;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        gridPosUI.SetActive(false);
    }

    /// <summary>
    /// Updates the hovered tile, UI, and highlight based on the mouse position.
    /// </summary>
    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.TryGetComponent(out GridTileBase tile))
        {
            if (tile != _currentTile)
            {
                ClearCurrentTile();

                _currentTile = tile;

                gridPosUI.SetActive(true);
                gridPosUI.transform.position = tile.transform.position + offset;

                Vector2 pos = tile.Position;
                positionText.text = $"({pos.x + 1}, {pos.y + 1})";

                HighlightTile(tile);
            }

            return;
        }

        ClearCurrentTile();
        _currentTile = null;
        gridPosUI.SetActive(false);
    }

    /// <summary>
    /// Applies color and size adjustment to currently selected tile.
    /// </summary>
    /// <param name="tile"></param>
    private void HighlightTile(GridTileBase tile)
    {
        _currentRenderer = tile.GetComponent<Renderer>();

        if (_currentRenderer != null)
        {
            _originalColor = _currentRenderer.material.color;
            _currentRenderer.material.color = hoverColor;
        }

        _originalScale = tile.transform.localScale;
        tile.transform.localScale = _originalScale * hoverScale;
    }

    /// <summary>
    /// Restores the tile to its default state.
    /// </summary>
    private void ClearCurrentTile()
    {
        if (_currentTile == null)
            return;

        if (_currentRenderer != null)
            _currentRenderer.material.color = _originalColor;

        _currentTile.transform.localScale = _originalScale;

        _currentRenderer = null;
    }
}