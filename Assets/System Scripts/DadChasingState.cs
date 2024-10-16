using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "DadChasingState", menuName = "SO/DadChasingState")]
public class DadChasingState : AUnitState
{
    public const int StateId = 0;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float knockbackForce = 20f;
    private float distanceToFlip = 3f;
    private float distanceTraveled = 0;
    private Vector2 lastPosition = Vector2.one * 9999;

    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;
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
        Vector2 difference = (unit.PlayerReference.transform.position - unit.transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        unit.PlayerReference.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        unit.SwitchState(DadBeltState.StateId);
    }
}
