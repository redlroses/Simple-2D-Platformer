using UnityEngine;

public class GroundObserver : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float _radius = default;
    [SerializeField] private LayerMask _groundMask = default;
    
    private bool _isGrounded = default;
    private Vector2 _normal = default;

    public bool IsGrounded => GetGroundCollider() != null;
    public Vector2 Normal => _normal;

    private void FixedUpdate()
    {
        var ground = Physics2D.CircleCast(transform.position, _radius, Vector2.down,
            0f, _groundMask);

        _normal = ground.normal;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, new Vector3(_normal.y, -_normal.x));
    }

    private Collider2D GetGroundCollider()
    {
        return Physics2D.OverlapCircle(transform.position, _radius, _groundMask);
    }
}