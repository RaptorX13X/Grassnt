using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardRotator : MonoBehaviour
{
    [SerializeField] private float attackDuration;
    [SerializeField] private float swingAngle;
    private bool swingRight;
    [SerializeField] private Collider2D attackCollider;
    private float timer;
    public bool attackInProgress;
    [SerializeField] private AudioClip swingSound;

    private void Update()
    {
        if (!attackInProgress) return;
        timer += Time.deltaTime;
        float desiredAngle = Mathf.Lerp(-swingAngle / 2, swingAngle / 2, timer / attackDuration);
        if (!swingRight) desiredAngle = -desiredAngle;
        transform.localEulerAngles = Vector3.forward * desiredAngle;
        if (timer > attackDuration)
        {
            attackInProgress = false;
            swingRight = !swingRight;
            attackCollider.enabled = false;
        }
    }
    public void StartAttack(Vector3 mousePos)
    {
        if (attackInProgress) return;

        transform.parent.up = (mousePos - transform.position).normalized;
        attackInProgress = true;
        timer = 0;
        attackCollider.enabled = true;
        AudioManager.instance.PlaySound(swingSound);
    }
}
