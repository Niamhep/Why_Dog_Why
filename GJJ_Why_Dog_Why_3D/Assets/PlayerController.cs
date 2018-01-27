using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    void Update ()
    {
        float movementAxis = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        // L/R = Left - Right Movement [X-Axis]
        // U/D = Up - Down Movement    [Y-Axis]
        // I/O = In - Out Movement     [Z-Axis]
        //                             L/R U/D I/O
        movement += new Vector3(movementAxis, transform.position.y, 0f);
	}

    private void LateUpdate()
    {
        rb.MovePosition(movement);
    }
}
