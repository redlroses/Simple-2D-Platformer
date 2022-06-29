using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]

public class PlayerInput : MonoBehaviour
{
    private IMovable _playerMover = default;
    private PlayerAnimator _playerAnimator = default;
    private float _horizontalMove = default;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        _horizontalMove = Input.GetAxis("Horizontal");

        if (Mathf.Abs(_horizontalMove) > Mathf.Epsilon)
        {
            _playerMover.Move(_horizontalMove);
        }
        
        if (Input.GetButton("Jump"))
        {
            _playerMover.Jump();
        }
    }
}
