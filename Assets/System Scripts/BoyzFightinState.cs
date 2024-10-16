using UnityEngine;

[CreateAssetMenu(fileName = "BoyzFightinState", menuName = "SO/BoyzFightinState")]
public class BoyzFightinState : AUnitState
{
    public const int StateId = 2;

    [SerializeField] private float speed = 3.5f;
    private float distanceToFlip = 3f;
    private float distanceTraveled = 0f;
    private Vector2 lastPosition = Vector2.one * 9999;

    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;
        unit.GetComponentInChildren<BatterBehaviour>().ResetCooldown();
    }

    public override void FixedUpdateState(Unit unit)
    {
        unit.Rigidbody2D.MovePosition(Vector2.MoveTowards(unit.transform.position, unit.PlayerReference.transform.position, speed * Time.fixedDeltaTime));
        if (lastPosition == Vector2.one * 9999)
        {
            lastPosition = unit.Rigidbody2D.position;
            return;
        }
        distanceTraveled += (unit.Rigidbody2D.position - lastPosition).magnitude;
        lastPosition = unit.Rigidbody2D.position;
        if (distanceTraveled >= distanceToFlip)
        {
            distanceTraveled = 0;
            unit.SpriteRenderer.flipX = !unit.SpriteRenderer.flipX;
        }

    }

    public override void UpdateState(Unit unit)
    {
        // Nothing
    }

    public override void OnCollisionEnterAction(Unit unit, Collision2D collision)
    {
        // Nothing
    }
}
