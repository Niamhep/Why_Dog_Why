using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber;

    public Touching Grounder;
    public Touching WallHugger;
    public Touching ObjectGrabber;
    public List<Collider> touchingColliders;

    public const float MaxHorizontalVelocity = 20f;
    public const float HorizontalAcceleration = 200f;
    public const float HorizontalAccelerationStageTwo = 20f;
    public const float HorizontalAccelerationThreshold = 14f;
    public const float HorizontalDeceleration = 200f;

    public const float HorizontalAccelerationAir = 20f;
    public const float HorizontalAccelerationAirStageTwo = 10f;
    public const float HorizontalDecelerationAir = 40f;
    public const float GroundRaycastLength = 0.1f;

    public const float JumpVelocity = 20;
    public const float WallJumpVelocity = 12;

    private Player _player;
    private Rigidbody _rigidbody;

    private Vector2 _moveAxis;
    private float _horizontalVelocity;

    public bool CanJump { get { return !_jumping && _isGrounded; } }

    private bool _jumpDown;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isGrabbing;
    private bool _jumping;

    private void Start()
    {
        _player = ReInput.players.GetPlayer(PlayerNumber);
        _rigidbody = GetComponent<Rigidbody> ();
        touchingColliders = ObjectGrabber._touchingColliders;
    }

    private void Update()
    {
        if (_isGrabbing && _player.GetButtonDown("Grab"))
        {

        }

        if(_player.GetButtonDown("Jump"))
        {
            if (CanJump)
            {
                Debug.Log("Normal");
                _jumping = true;
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, JumpVelocity, _rigidbody.velocity.z);
            }
            else if (_isTouchingWall)
            {
                foreach(Collider col in WallHugger._touchingColliders)
                {
                    Vector3 touchingPosition = col.ClosestPointOnBounds(transform.position);
                    Vector3 normal = touchingPosition - transform.position;

                    if(Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
                    {
                        _jumping = true;
                        _horizontalVelocity += -Mathf.Sign(normal.x) * WallJumpVelocity;
                        _rigidbody.velocity = new Vector3(_horizontalVelocity, JumpVelocity, _rigidbody.velocity.z);
                        break;
                    }
                }
            }
        }

        if(_jumping && _player.GetButtonUp("Jump"))
        {
            _jumping = false;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Mathf.Min(_rigidbody.velocity.y, 0), _rigidbody.velocity.z);
        }

        _moveAxis = _player.GetAxis2D("MoveX", "MoveY");

    }

    private Ray _groundRaycast;
    private RaycastHit[] _results;
    private void FixedUpdate()
    {
        _isGrounded = Grounder.IsTouching;
        _isTouchingWall = WallHugger.IsTouching;
        _isGrabbing = ObjectGrabber.IsTouching;

        _horizontalVelocity = _rigidbody.velocity.x;

        if (Mathf.Abs(_moveAxis.x) > 0.2f)
        {
            if (_isGrounded)
            {
                if (_moveAxis.x > 0 && _horizontalVelocity < HorizontalAccelerationThreshold ||
                    _moveAxis.x < 0 && _horizontalVelocity > -HorizontalAccelerationThreshold)
                {
                    _horizontalVelocity += _moveAxis.x * HorizontalAcceleration * Time.fixedDeltaTime;
                }
                else
                {
                    _horizontalVelocity += _moveAxis.x * HorizontalAccelerationStageTwo * Time.fixedDeltaTime;
                }
            }
            else
            {
                if (_moveAxis.x > 0 && _horizontalVelocity < HorizontalAccelerationThreshold ||
                    _moveAxis.x < 0 && _horizontalVelocity > -HorizontalAccelerationThreshold)
                {
                    _horizontalVelocity += _moveAxis.x * HorizontalAccelerationAir * Time.fixedDeltaTime;
                }
                else
                {
                    _horizontalVelocity += _moveAxis.x * HorizontalAccelerationAirStageTwo * Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            float increment = 0;
            if (_isGrounded)
            {
                increment = HorizontalDeceleration * Time.fixedDeltaTime;
            }
            else
            {
                increment = HorizontalDecelerationAir * Time.fixedDeltaTime;
            }
            if (_horizontalVelocity > increment)
            {
                _horizontalVelocity -= increment;
            }
            else if (_horizontalVelocity < -increment)
            {
                _horizontalVelocity += increment;
            }
            else
            {
                _horizontalVelocity = 0;
            }
        }

        _horizontalVelocity = Mathf.Clamp(_horizontalVelocity, -MaxHorizontalVelocity, MaxHorizontalVelocity);

        _rigidbody.velocity = new Vector3(_horizontalVelocity, _rigidbody.velocity.y, 0);

    }
}
