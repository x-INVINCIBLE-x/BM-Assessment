using UnityEngine;

/// <summary>
/// Grid-controlled player that moves to the clicked tile.
/// </summary>
public class PlayerAgent : GridAgent
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    /// <summary>
    /// Handles mouse input and requests movement to the selected grid tile.
    /// </summary>
    private void Update()
    {
        if (IsMoving) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.TryGetComponent(out GridTileBase tile))
        {
            MoveTo(tile.Position);
        }
    }
}
