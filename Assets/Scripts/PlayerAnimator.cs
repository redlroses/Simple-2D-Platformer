using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private GroundObserver _groundObserver = default;

    private Animator _animator = default;
    private SpriteRenderer _spriteRenderer = default;

    private readonly int _jump = Animator.StringToHash("Jump");
    private readonly int _speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Jump()
    {
        if (_groundObserver.IsGrounded)
            _animator.SetTrigger(_jump);
    }

    public void SetSpeed(float speed)
    {
        SetFlipX(speed < 0f);
        _animator.SetFloat(_speed, Mathf.Abs(speed));
    }

    private void SetFlipX(bool flipX)
    {
        _spriteRenderer.flipX = flipX;
    }
}
