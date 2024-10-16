using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamageOnPlayerCollision(collision.collider);
    }

    private void DealDamageOnPlayerCollision(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.TryGetComponent(out Health health))
        {
            health.Kill();
        }
    }
}
