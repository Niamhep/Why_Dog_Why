using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public BoxCollider Bounds;
    public float MoveSpeed = 10;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 axis)
    {
        Vector3 movement = new Vector3(axis.x, axis.y, 0) * MoveSpeed * Time.deltaTime;
        _rigidbody.MovePosition(new Vector3(    Mathf.Clamp(_rigidbody.position.x + movement.x, Bounds.bounds.min.x, Bounds.bounds.max.x),
                                                Mathf.Clamp(_rigidbody.position.y + movement.y, Bounds.bounds.min.y, Bounds.bounds.max.y),
                                            transform.position.z));
    }
}
