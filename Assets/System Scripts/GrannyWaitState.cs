using UnityEngine;

[CreateAssetMenu(fileName = "GrannyWaitState", menuName = "SO/GrannyWaitState")]
public class GrannyWaitState : AUnitState
{
    public const int StateId = 0;

    [SerializeField] private float waitingTime = 5f;
    private float alreadyWaited;
    private float flipTimer;

    public override void EnterState(Unit unit)
    {
        GrampsBrain.Instance.SetGranny(unit);

        alreadyWaited = 0f;
    }

    public override void FixedUpdateState(Unit unit)
    {
        flipTimer -= Time.fixedDeltaTime;
        if (flipTimer <= 0)
        {
            flipTimer = Random.Range(.3f, 1f);
            unit.SpriteRenderer.flipX = !unit.SpriteRenderer.flipX;
        }
    }

    public override void UpdateState(Unit unit)
    {
        alreadyWaited += Time.deltaTime;

        if (alreadyWaited >= waitingTime)
        {
            unit.SwitchState(GrannyHealingState.StateId);
        }
    }

    public override void OnCollisionEnterAction(Unit unit, Collision2D collision)
    {
        // Nothing
    }
}
