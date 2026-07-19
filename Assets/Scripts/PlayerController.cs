using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GridAgent agent;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (agent.IsMoving) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.TryGetComponent(out GridTileBase tile))
        {
            agent.MoveTo(tile.Position);
        }
    }
}
