using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{

    public float angle = 40f;
    public float lineLength = 2f;

    [SerializeField]
    protected LineRenderer lr;

    float RadToDeg(float rads)
    {
        return (rads * (180 / Mathf.PI));
    }

    float GetAngle(Vector2 origin, Vector2 endPoint)
    {
        float deltaX = endPoint.x - origin.x;
        float deltaY = endPoint.y - origin.y;
        float rads = Mathf.Atan2(deltaX, deltaY);
        return RadToDeg(rads);
    }

    // Get Position From Angle and Distance
    Vector2 GetPosition(float angle, float distance)
    {
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    void Start()
    {
        // Get Line Renderer Reference
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector2 posAtAngle = GetPosition(angle, lineLength);

        lr.SetPosition(1, new Vector3(GetPosition(angle, lineLength).x, GetPosition(angle, lineLength).y, 0f));
    }
}
