using Rewired;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber;
    public Transform GroundedRaycast;

    public const float MaxHorizontalVelocity = 30f;
    public const float HorizontalAcceleration = 500f;

    private Player _player;
    private Rigidbody _rigidbody;

    private Vector2 moveAxis;
    private float _horizontalVelocity;
    private bool _isGrounded;

    private void Start()
    {
        _player = ReInput.players.GetPlayer(PlayerNumber);
        _rigidbody = GetComponent<Rigidbody> ();
    }

    private void Update()
    {
        _player.GetButtonDown("Jump");

        moveAxis = _player.GetAxis2D("MoveX", "MoveY");

    }

    private void FixedUpdate()
    {
        _horizontalVelocity = _rigidbody.velocity.x;

        _horizontalVelocity += moveAxis.x * HorizontalAcceleration * Time.fixedDeltaTime;
        _horizontalVelocity = Mathf.Clamp(_horizontalVelocity, -MaxHorizontalVelocity, MaxHorizontalVelocity);

        _rigidbody.velocity = new Vector3(_horizontalVelocity, _rigidbody.velocity.y, 0);
    }
}
