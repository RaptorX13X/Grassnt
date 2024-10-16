using UnityEngine;

[CreateAssetMenu(fileName = "GrandpaFightingState", menuName = "SO/GrandpaFightingState")]
public class GrandpaFightingState : AUnitState
{
    public const int StateId = 0;

    [SerializeField] private float baseSpeed = 3.5f;
    [SerializeField] private float maxSpeed = 5.5f;
    private float distanceToFlip = 3f;
    private float distanceTraveled = 0f;
    private Vector2 lastPosition = Vector2.one * 9999;
    public override void EnterState(Unit unit)
    {
        GrampsBrain.Instance.SetGrandpa(unit);
        unit.SpriteRenderer.sprite = stateSprite;
        unit.GetComponentInChildren<GrandpaBattingBehaviour>().ResetCooldown();
    }

    public override void FixedUpdateState(Unit unit)
    {
        unit.Rigidbody2D.MovePosition(Vector2.MoveTowards(unit.transform.position, unit.PlayerReference.transform.position, GrampsBrain.Instance.GrandpaScaling(baseSpeed, maxSpeed) * Time.fixedDeltaTime));
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
