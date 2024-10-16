using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMenager playerMenager))
            return;
        if(other.gameObject.TryGetComponent(out Health health)) health.Damage();
        Destroy(gameObject);
    }
}
