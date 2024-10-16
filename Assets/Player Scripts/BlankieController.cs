using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlankieController : MonoBehaviour
{
    [SerializeField] private float attackDuration;
    [SerializeField] private float hideDuration;
    [SerializeField] private float attackRange;
    [SerializeField] private Collider2D attackCollider;
    private float timer;
    private int attackInProgress;
    [SerializeField] private AudioClip whipSound;

    private void Update()
    {
        if (attackInProgress == 0) return;
        timer += Time.deltaTime;
        if (attackInProgress == 1)
        {
            transform.localScale = new Vector3(1,Mathf.Lerp(0, 1, timer / hideDuration), 1);
            if (timer > attackDuration)
            {
                attackInProgress = 2;
                timer = 0;
            }
            return;
        }

        transform.localScale = new Vector3(1,Mathf.Lerp(1, 0, timer / hideDuration), 1);
        if (timer > hideDuration)
        {
            attackInProgress = 0;
            timer = 0;
            attackCollider.enabled = false;
        }
    }

    public void StartAttack(Vector3 referencePoint)
    {
        if (attackInProgress != 0) return;

        transform.up = (referencePoint - transform.position).normalized;

        attackInProgress = 1;
        timer = 0;
        attackCollider.enabled = true;
        AudioManager.instance.PlaySound(whipSound);
    }
}
