using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour
{
    public Mover Mover;

    public void Move(Vector2 axis)
    {
        if (Mover != null)
        {
            Mover.Move(axis);
        }
    }
}
