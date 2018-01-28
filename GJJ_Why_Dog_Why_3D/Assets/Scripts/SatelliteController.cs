using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    public Transform Satellite;

    private const float RotateSpeed = 10;

    public void Move(Vector2 axis)
    {
        if(Satellite != null)
        {
            Satellite.Rotate(new Vector3(0, 0, -axis.x * RotateSpeed * Time.deltaTime));
        }
    }
}
