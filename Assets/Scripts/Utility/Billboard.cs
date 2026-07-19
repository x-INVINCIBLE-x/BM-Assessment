using UnityEngine;

/// <summary>
/// Rotates transform of the object to face camera.
/// </summary>
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