using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public LayerMask LayerMask;
    private LineRenderer _lineRenderer;

    private const float Width = 0.2f;
    private const float MaxDistance = 100f;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DoRaycasts(Transform relay)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);
        DoRaycastsRecursive(relay, ref points);

        Debug.Log(points.Count);

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    private void DoRaycastsRecursive(Transform relay, ref List<Vector3> points)
    {
        Ray raycast = new Ray(relay.position, relay.forward);
        List<RaycastHit> firstRayCast = new List<RaycastHit>(Physics.RaycastAll(raycast, MaxDistance, LayerMask));

        if (firstRayCast.Count > 0)
        {
            OnRaycastHit(firstRayCast[0], ref points);
            return;
        }

        raycast = new Ray(relay.position + relay.right * Width, relay.forward);
        List<RaycastHit> secondRayCast = new List<RaycastHit>(Physics.RaycastAll(raycast, MaxDistance, LayerMask));

        if (secondRayCast.Count > 0)
        {
            OnRaycastHit(secondRayCast[0], ref points);
            return;
        }

        raycast = new Ray(relay.position - relay.right * Width, relay.forward);
        List<RaycastHit> thirdRayCast = new List<RaycastHit>(Physics.RaycastAll(raycast, MaxDistance, LayerMask));

        if (thirdRayCast.Count > 0)
        {
            OnRaycastHit(thirdRayCast[0], ref points);
            return;
        }
    }

    private void OnRaycastHit(RaycastHit hit, ref List<Vector3> points)
    {
        Debug.Log("Hit: " + hit.point);
        points.Add(hit.point);

        Relay relayHit = hit.transform.GetComponent<Relay>();
        if(relayHit != null)
        {
            DoRaycastsRecursive(relayHit.SignalSpawnPoint, ref points);
        }
    }

    public void StartSignal()
    {
        _lineRenderer.enabled = true;
    }
}
