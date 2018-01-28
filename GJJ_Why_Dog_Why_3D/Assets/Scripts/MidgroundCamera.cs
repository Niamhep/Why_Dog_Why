using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidgroundCamera : MonoBehaviour
{
    public CameraFollow MainCamera;

    private Camera _camera;

    public float MinScale;
    public float MaxScale;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.orthographicSize = Mathf.Lerp(MinScale, MaxScale, MainCamera.NormalisedZoom);
    }
}
