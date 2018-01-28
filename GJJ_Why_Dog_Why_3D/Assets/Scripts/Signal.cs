using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public LayerMask LayerMask;
    private LineRenderer _lineRenderer;

    private const float Width = 0.1f;
    private const float MaxDistance = 100f;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DoRaycasts(Transform relay)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);
        List<Relay> relays = new List<Relay>();
        DoRaycastsRecursive(relay, ref points, ref relays);

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    private void DoRaycastsRecursive(Transform relay, ref List<Vector3> points, ref List<Relay> relays)
    {
        Ray raycast = new Ray(relay.position, relay.forward * MaxDistance);


        RaycastHit hit;
        if(Physics.Raycast(raycast, out hit, MaxDistance, LayerMask))
        {
            OnRaycastHit(hit, ref points, ref relays);
            return;
        }

        raycast = new Ray(relay.position + relay.right * Width, relay.forward);
        if (Physics.Raycast(raycast, out hit, MaxDistance, LayerMask))
        {
            OnRaycastHit(hit, ref points, ref relays);
            return;
        }

        raycast = new Ray(relay.position - relay.right * Width, relay.forward);
        if (Physics.Raycast(raycast, out hit, MaxDistance, LayerMask))
        {
            OnRaycastHit(hit, ref points, ref relays);
            return;
        }
    }

    private void OnRaycastHit(RaycastHit hit, ref List<Vector3> points, ref List<Relay> relays)
    {
        points.Add(hit.point);

        PlayerController player = hit.transform.GetComponent<PlayerController>();
        if(player != null)
        {
            player.Kill();
            return;
        }

        Relay relayHit = hit.transform.GetComponent<Relay>();
        if(relayHit != null)
        {
            if (!relays.Contains(relayHit) && !relayHit.SpawnSignal)
            {
                relays.Add(relayHit);
                DoRaycastsRecursive(relayHit.SignalSpawnPoint, ref points, ref relays);
            }
        }
    }

    public void StartSignal()
    {
        _lineRenderer.enabled = true;
    }
}
