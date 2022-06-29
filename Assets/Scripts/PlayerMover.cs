using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private GroundObserver _groundObserver = default;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalDirection)
    {
        var direction = new Vector2(horizontalDirection * _speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = direction;
    }

    public void Jump()
    {
        Debug.Log(_groundObserver.IsGrounded);
    }
}