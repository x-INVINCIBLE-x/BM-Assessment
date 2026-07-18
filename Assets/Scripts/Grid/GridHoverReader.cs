using UnityEngine;
using TMPro;

public class GridHoverReader : MonoBehaviour
{
    [SerializeField] private GameObject gridPosUI;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private Vector3 offset;
    
    private GridTileBase _currentTile;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        gridPosUI.SetActive(false);
    }


    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.TryGetComponent(out GridTileBase tile))
        {
            if (tile != _currentTile)
            {
                _currentTile = tile;

                gridPosUI.SetActive(true);
                gridPosUI.transform.position = tile.transform.position + offset;

                Vector2 pos = tile.Position;
                positionText.text = $"({pos.x + 1}, {pos.y + 1})";
            }

            return;
        }

        _currentTile = null;
        gridPosUI.SetActive(false);
    }
}