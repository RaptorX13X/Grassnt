using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> hearts;
    
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        playerHealth.OnDamaged += ShowHealthChange;
    }

    private void ShowHealthChange()
    {
        if (hearts.Count == 0)
            return;
        var heartsToDestroy = hearts[hearts.Count - 1];
        hearts.Remove(heartsToDestroy);
        Destroy(heartsToDestroy);
    }
}
