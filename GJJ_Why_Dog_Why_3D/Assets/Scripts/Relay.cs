using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
    public Signal Signal;
    public Transform SignalSpawnPoint;

    public bool SpawnSignal;

    public void Start()
    {
        if(SpawnSignal)
        {
            Signal.StartSignal();
        }
    }

    private void Update()
    {
        if(SpawnSignal)
        {
            Signal.DoRaycasts(SignalSpawnPoint);
        }
    }
}
