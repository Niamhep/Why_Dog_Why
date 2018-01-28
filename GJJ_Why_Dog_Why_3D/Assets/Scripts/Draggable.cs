using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Rigidbody _target;
    public bool IsBeingDragged
    {
        get; private set;
    }

    private Rigidbody _rigidbody;

    private const float DragForce = 2000;
    private const float TargetVelocityForce = 150;
    private const float DragDamping = 300;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Rigidbody target)
    {
        IsBeingDragged = true;
        _target = target;
    }

    public void Release()
    {
        IsBeingDragged = false;
        _target = null;
    }

    private void FixedUpdate()
    {
        if (IsBeingDragged && _target != null)
        {
            Vector3 forceDirection = _target.position - _rigidbody.position;

            Vector3 force = (forceDirection * DragForce) + _target.velocity * TargetVelocityForce;
            force = force - Vector3.Project(_rigidbody.velocity, force.normalized) * DragDamping;

            _rigidbody.AddForce(force);
        }
    }
}
