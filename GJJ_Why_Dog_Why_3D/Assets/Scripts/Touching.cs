using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touching : MonoBehaviour
{
    public LayerMask LayerMask;
    public List<Collider> _touchingColliders = new List<Collider>();

    public bool IsTouching { get { return _touchingColliders.Count > 0; } }

    private void OnTriggerEnter(Collider col)
    {
        if (LayerMask == (LayerMask | (1 << col.gameObject.layer)))
        {
            _touchingColliders.Add(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (LayerMask == (LayerMask | (1 << col.gameObject.layer)))
        {
            _touchingColliders.Remove(col);
        }
    }
}
