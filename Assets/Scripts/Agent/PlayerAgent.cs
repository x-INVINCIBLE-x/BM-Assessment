using UnityEngine;

public class PlayerAgent : GridAgent
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

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
