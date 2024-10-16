using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private ParticleSystem feathers;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMenager playerMenager))
            return;

        RaycastHit2D[] pillowHits = Physics2D.CircleCastAll(transform.position, radius, Vector3.forward);
        foreach (RaycastHit2D hit in pillowHits)
        {
            if (hit.collider.gameObject.TryGetComponent(out PlayerMenager menager)) continue;
            if (hit.collider.gameObject.TryGetComponent(out Health health)) health.Damage();
        }
        ExplodePillow();
    }

    private void ExplodePillow()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        feathers.Play();
        Invoke("DestroyMe", 1f);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
