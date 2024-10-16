using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private float damagingPeriodSeconds = 0.7f;

    private float timeToNextDamageDeal = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamageOnPlayerCollision(collision.collider);
    }

    // for klapek only
    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamageOnPlayerCollision(other);
    }


    private void DealDamageOnPlayerCollision(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.TryGetComponent(out Health health))
        {
            timeToNextDamageDeal = damagingPeriodSeconds;

            health.Damage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timeToNextDamageDeal <= 0f)
        {
            DealDamageOnPlayerCollision(collision.collider);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (timeToNextDamageDeal <= 0f)
        {
            DealDamageOnPlayerCollision(other);
        }
    }

    private void Update()
    {
        timeToNextDamageDeal -= Time.deltaTime;
    }
}
