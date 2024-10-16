using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemnet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float acceleratione;
    public void Move(Vector2 input, Vector3 mousePos)
    {
        var newVelocity = rb.velocity + acceleratione * Time.fixedDeltaTime * input.normalized;
        if (newVelocity.magnitude <= speed)
        {
            rb.velocity = newVelocity;
        }
    }

    public void SetAnimSpeed(Animator animator)
    {
        animator.SetFloat("Speed", rb.velocity.magnitude / speed);
    }
}
