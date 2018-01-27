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
    public const float HorizontalAcceleration = 50f;
    public const float GroundRaycastLength = 0.1f;

    public const float JumpVelocity = 20;

    private Player _player;
    private Rigidbody _rigidbody;

    private Vector2 _moveAxis;
    private float _horizontalVelocity;

    public bool CanJump { get { return !_jumping && (_isGrounded || _isTouchingWall); } }

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
        // Object Grabbing
        if (_isGrabbing && _player.GetButtonDown("Grab"))
        {

        }
        
        if(CanJump && _player.GetButtonDown("Jump"))
        {
            _jumping = true;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, JumpVelocity, _rigidbody.velocity.z);
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

        _horizontalVelocity += _moveAxis.x * HorizontalAcceleration * Time.fixedDeltaTime;
        _horizontalVelocity = Mathf.Clamp(_horizontalVelocity, -MaxHorizontalVelocity, MaxHorizontalVelocity);

        _rigidbody.velocity = new Vector3(_horizontalVelocity, _rigidbody.velocity.y, 0);

    }
}
