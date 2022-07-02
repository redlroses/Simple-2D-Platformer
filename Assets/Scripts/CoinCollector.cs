using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int _coinsCollected = default;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin) == false)
            return;
        
        _coinsCollected++;
    }
}
