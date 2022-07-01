using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GroundObserver _groundObserver = default;
    [SerializeField] [Range(0f, 1f)] private float _climbableSlope;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalDirection)
    {
        Vector2 normal = _groundObserver.Normal;
        Vector2 directionAmongGround = new Vector2(normal.y, -normal.x);
        Vector2 direction;
        
        if (normal.y > _climbableSlope)
        {
            direction = new Vector2(directionAmongGround.x * horizontalDirection * _speed,
                directionAmongGround.y * horizontalDirection * _speed);
        }
        else
        {
            direction = new Vector2(horizontalDirection * _speed, _rigidbody2D.velocity.y);
        }
        
        _rigidbody2D.velocity = direction;
    }

    public void Jump()
    {
        if (_groundObserver.IsGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}