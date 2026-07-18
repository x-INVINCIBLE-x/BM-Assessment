using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = _mainCamera.transform.forward;
    }
}