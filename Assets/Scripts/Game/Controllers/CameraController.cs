using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 targetResolution;
    [SerializeField] private Camera _camera;

    private bool validated = false;

    private Vector2 lastResolution;

    void Start()
    {
        SetSize();
    }

    private void Update()
    {
        if (validated || lastResolution.x != Screen.width || lastResolution.y != Screen.height)
        {
            validated = false;

            SetSize();
        }
    }

    private void SetSize()
    {
        lastResolution.x = Screen.width;
        lastResolution.y = Screen.height;

        float targetRatio = targetResolution.x / targetResolution.y;
        float ratio = lastResolution.x / lastResolution.y;

        _camera.orthographicSize = 5 * (targetRatio / ratio);
    }

    void OnValidate()
    {
        validated = true;
    }
}
