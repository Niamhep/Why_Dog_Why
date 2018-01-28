using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> Players = new List<Transform>();
    private List<Rigidbody> _playerRigidbodies = new List<Rigidbody>();

    public float NormalisedZoom { get { return Mathf.Clamp01((transform.position.z - MinZ) / (MaxZ - MinZ)); } }

    private const float MinZ = -70f;
    private const float MaxZ = -160f;

    private const float CameraDampTime = 0.6f;
    private const float ZoomSpeed = 100f;
    private const float Padding = 4;
    private const float LookAheadMultiplier = 0.4f;

    private Camera _camera;

    private Vector3 _cameraTarget;

    private Bounds _playerBoundingBox;
    private Bounds _cameraViewBoundingBox;

    private Vector3 _velocity;
    private float zoom;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        foreach(Transform player in Players)
        {
            _playerRigidbodies.Add(player.GetComponent<Rigidbody>());
        }
        zoom = transform.position.z;
    }


    private void Update()
    {
        _playerBoundingBox = new Bounds(Players[0].position, Vector3.zero);

        for(int i=0; i<Players.Count; i++)
        {
            _playerBoundingBox.Encapsulate(Players[i].position);
            _playerBoundingBox.Encapsulate(Players[i].position +(_playerRigidbodies[i].velocity * LookAheadMultiplier));
        }

#if DEBUG
        //DrawBounds(_playerBoundingBox, Color.yellow);
#endif

        Vector3[] frustumCorners = new Vector3[4];
        _camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), zoom, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        _cameraViewBoundingBox = new Bounds(transform.TransformPoint(frustumCorners[0]), Vector3.zero);

        foreach (Vector3 corner in frustumCorners)
        {
            _cameraViewBoundingBox.Encapsulate(transform.TransformPoint(corner));
        }

#if DEBUG
        //DrawBounds(_cameraViewBoundingBox, Color.blue);
#endif
        //

        float viewPortSizeDifference = Mathf.Max((_playerBoundingBox.extents.x + Padding) - _cameraViewBoundingBox.extents.x, (_playerBoundingBox.extents.y + Padding) - _cameraViewBoundingBox.extents.y);
        if (viewPortSizeDifference > 1)
        {
            zoom -= ZoomSpeed * Time.deltaTime;
        }
        else if (viewPortSizeDifference < -1)
        {
            zoom += ZoomSpeed * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, MaxZ, MinZ);

        _cameraTarget = new Vector3(_playerBoundingBox.center.x, _playerBoundingBox.center.y, zoom);
        transform.position = Vector3.SmoothDamp(transform.position, _cameraTarget, ref _velocity, CameraDampTime);
    }

    private void DrawBounds(Bounds bounds, Color color)
    {
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, 0), new Vector3(bounds.min.x, bounds.max.y, 0), color);
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, 0), new Vector3(bounds.max.x, bounds.max.y, 0), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, 0), new Vector3(bounds.max.x, bounds.min.y, 0), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, 0), new Vector3(bounds.min.x, bounds.min.y, 0), color);
    }
}
