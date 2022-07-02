using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CoinCollector coinCollector) == false)
            return;
        
        Destroy(gameObject);
    }
}
