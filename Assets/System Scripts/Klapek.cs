using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(DamageOnContact))]
public class Klapek : MonoBehaviour
{
    [SerializeField] private float rotationAngle = 360f;
    [SerializeField] private float speed = 10f;

    private Vector2 target;
    private Rigidbody2D rb;
    private Unit thrower;
    private GameObject throwerObject;

    private bool returning = false;

    public void Init(Unit thrower, Vector2 target)
    {
        rb = GetComponent<Rigidbody2D>();

        this.thrower = thrower;
        this.target = target;
        throwerObject = thrower.gameObject;
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * rotationAngle * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        if (!returning)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime));

            if (Vector2.Distance(transform.position, target) <= 0.01f)
            {
                returning = true;
            }
        }
        else
        {
            if (throwerObject == null)
            {
                Destroy(gameObject);
                return;
            }

            rb.MovePosition(Vector2.MoveTowards(transform.position, thrower.transform.position, speed * Time.fixedDeltaTime));

            if (Vector2.Distance(transform.position, thrower.transform.position) <= 0.5f)
            {
                thrower.SwitchState(0);
                Destroy(gameObject);
            }
        }
    }
}
