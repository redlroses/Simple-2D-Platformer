#nullable enable
using JetBrains.Annotations;
using UnityEngine;

public class GroundObserver : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float _climbableSlope;
    [SerializeField] [Range(0f, 1f)] private float _radius = default;
    [SerializeField] private LayerMask _groundMask = default;

    private bool _isGrounded = default;
    private Vector2 _normal1 = default;
    private bool _isFlatGround = default;

    public bool IsGrounded => CheckIsInGround();
    public Vector2 Normal => _normal1;
    public bool IsFlatGround => _isFlatGround;

    private bool CheckIsInGround()
    {
        var groundCollider = GetGroundCollider();
        return groundCollider != null;
    }

    private bool CheckIsClimbableSlope()
    {
        var groundCollider = GetGroundCollider();
        
        //TODO: Написать проверку на разрешённый угол наклона поверхности
        
        return groundCollider is null;
    }

    [CanBeNull]
    private Collider2D GetGroundCollider()
    {
        return Physics2D.OverlapCircle(transform.position, _radius, _groundMask);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}