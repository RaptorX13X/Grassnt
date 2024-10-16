using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = .2f;
    [SerializeField] private float cooldown = 2f;

    public bool dashing;
    private float timer = 0f;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    [SerializeField] private AudioClip dashSound;
    [SerializeField] private SandevistanEffect se;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(dashing)
        {
            se.UpdateSDVSTN(rb.velocity.normalized);
            if (timer <= 0f)
            {
                dashing = false;
                rb.velocity = Vector2.zero;
                timer = cooldown;
                playerCollider.enabled = true;
            }
            else
            {
                rb.velocity = rb.velocity.normalized * dashSpeed;
            }
        }
    }

    public void TryApplyDash(Vector3 mousePosition, Vector2 input)
    {
        if (timer > 0f)
            return;


        playerCollider.enabled = false;
        timer = dashDuration;
        Vector2 difference = (input == Vector2.zero) ? (mousePosition - transform.position).normalized : input.normalized;
        rb.velocity = difference * dashSpeed;
        dashing = true;
        AudioManager.instance.PlaySound(dashSound);
    }
}