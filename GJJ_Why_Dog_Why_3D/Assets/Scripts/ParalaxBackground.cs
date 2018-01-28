using UnityEngine;
using System.Collections;

public class ParalaxBackground : MonoBehaviour
{
    public int materialIndex = 0;
    public Transform TrackingObject;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);

    private Vector2 uvOffset = Vector2.zero;
    private Renderer _renderer;

    private Vector3 _trackedMovement;
    private Vector3 _lastTrackedPosition;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _lastTrackedPosition = TrackingObject.transform.position;
    }

    void LateUpdate()
    {
        _trackedMovement = TrackingObject.position - _lastTrackedPosition;
        _lastTrackedPosition = TrackingObject.position;

        uvOffset += (new Vector2(uvAnimationRate.x * _trackedMovement.x, uvAnimationRate.y * _trackedMovement.y) * Time.deltaTime);
        uvOffset.x = uvOffset.x % 1;
        uvOffset.y = uvOffset.y % 1;

        if (_renderer.enabled)
        {
            _renderer.material.mainTextureOffset = uvOffset;
        }
    }
}