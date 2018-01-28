using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDriver : MonoBehaviour
{
    private Animator _animator;
    private const string SpeedParam = "Speed";

    private void Update()
    {
        _animator = GetComponent<Animator>();
    }

    public float Speed
    {
        get
        {
            return _animator.GetFloat(SpeedParam);
        }
        set
        {
            _animator.SetFloat(SpeedParam, value);
        }
    }
}
